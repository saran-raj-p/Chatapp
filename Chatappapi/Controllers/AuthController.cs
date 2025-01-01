using Chatappapi.Interface;
using Chatappapi.Model;
using Chatappapi.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chatappapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _Authentication;
        private readonly AuthServices _AuthServices;
        private readonly IEmailService _emailService;

        public AuthController(IAuthentication authentication,AuthServices authServices, IEmailService emailService)
        {
            _Authentication = authentication;
            _AuthServices = authServices;
            _emailService = emailService;
        
        
        }
        [HttpPost("UserRegistration")]
        public async Task<IActionResult> Registration(RegisterDto userdata)
        {
            try
            {
                var result = await _Authentication.UserRegister(userdata);
                if (result != null)
                {

                    /*LoginDTo user = new LoginDTo();
                    user.Email = userdata.Email;
                    user.Password = userdata.Password;
                    await UserLogin(user);*/
                    return Ok(new {Message="User Registration Sucessful"});
                }
                return StatusCode(404,new {Message="Invalid Email or Password"});
            }
            catch (Exception ex)
            {
                return StatusCode(500,new {Message ="Internal Server Error"});
            }
            
        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginDTo userdata)
        {
            try
            {

                var checkIfUserExists = await _Authentication.UserData(userdata);

                if (checkIfUserExists != null)
                {
                    var AuthToken = _AuthServices.GenerateToken(checkIfUserExists);
                    var refreshtoken = _AuthServices.generateRefreshToken(AuthToken);
                    _Authentication.saveRefreshToken(refreshtoken, checkIfUserExists.UserId);
                    return Ok(new { AccessToken = AuthToken, RefreshToken = refreshtoken });
                }
                else {
                    return StatusCode(500);
                }
                
            }
            catch (Exception ex) {
                return Unauthorized("Invalid Username or Password");
             }
        }
        [HttpPost("UserActivation")]
        public async Task<ActionResult> UserActivation(Getotp getotp)
        {
            try
            {
                await _Authentication.UserActivation(getotp);
                return Ok(new { Message = "User successfully activated." });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = "Invaild otp" });
            }
        }
        [HttpPost("ExpiredToken")]
        public async Task<ActionResult> RegenerateAccessToken(String token)
        {
            try
            {
                /*var validate = await _Authentication.validateRefreshToken(token);
                if (validate == true)
                {*/
                    ClaimsPrincipal principal =  _AuthServices.claimsPrincipalFrom(token);
                    var accessToken = _AuthServices.generateAccessToken(principal);
                    return Ok(new { AccessToken = accessToken });

                /*}
                else
                {
                    return NotFound(new { Message = "Invalid Token" });
                }*/
            }
            catch (Exception ex) {
                return StatusCode(500, new {ex.Message});   
            }
            
        }

        [HttpPost("sendotp")]
        public async Task<IActionResult> SendOtp( EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || !IsValidEmail(request.Email))
            {
                return BadRequest(new { Message = "Invalid email address." });
            }

            var isEmailRegistered = await _Authentication.IsEmailRegisteredAsync(request.Email);
            if (!isEmailRegistered)
            {
                return BadRequest(new { Message = "Email is not registered." });
            }

            var otpSent = await _Authentication.SendOtpAsync(request.Email);
            if (!otpSent)
            {
                return BadRequest(new { Message = "Failed to send OTP. Please try again." });
            }

            return Ok(new { Message = "OTP has been sent to your email." });
        }

        [HttpPost("verifyotp")]
        public async Task<IActionResult> VerifyOtp( OtpRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Otp))
            {
                return BadRequest(new { Message = "Email and OTP fields cannot be empty." });
            }

            var isValidOtp = await _Authentication.VerifyOtpAsync(request.Email, request.Otp);
            if (!isValidOtp)
            {
                return BadRequest(new { Message = "Invalid or expired OTP." });
            }

            return Ok(new { Message = "OTP verified successfully." });
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword( ForgotDTo request)
        {
            if (string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.NewPassword) ||
                string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BadRequest(new { Message = "Fields cannot be empty." });
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest(new { Message = "Passwords do not match." });
            }

            if (request.NewPassword.Length < 8)
            {
                return BadRequest(new { Message = "Password must be at least 8 characters long." });
            }

            var result = await _Authentication.ResetPasswordAsync(request.Email, request.NewPassword);

            if (!result)
            {
                return BadRequest(new { Message = "Unable to reset password. Please try again." });
            }

            return Ok(new { Message = "Password has been successfully updated." });
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}
    

