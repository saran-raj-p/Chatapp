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
        //private readonly IEmailService _emailService;

        public AuthController(IAuthentication authentication,AuthServices authServices)
        {
            _Authentication = authentication;
            _AuthServices = authServices;
           // _emailService = emailService;
        
        
        }
        [HttpPost("UserRegistration")]
        public async Task<IActionResult> Registration(RegisterDto userdata)
        {
            try
            {
                if(!string.IsNullOrEmpty(userdata.Name)&& !string.IsNullOrEmpty(userdata.Email)&& !string.IsNullOrEmpty(userdata.Phone)&& !string.IsNullOrEmpty(userdata.Password))
                {
                    var result = await _Authentication.UserRegister(userdata);
                    if (result != null)
                    {

                        /*LoginDTo user = new LoginDTo();
                        user.Email = userdata.Email;
                        user.Password = userdata.Password;
                        await UserLogin(user);*/
                        return Ok(new { Message = "User Registration Sucessful" });
                    }
                }
                
               
                
                return BadRequest();
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
                var validate = await _Authentication.validateRefreshToken(token);
                if (validate == true)
                {
                    ClaimsPrincipal principal =  _AuthServices.claimsPrincipalFrom(token);
                    var accessToken = _AuthServices.generateAccessToken(principal);
                    return Ok(new { AccessToken = accessToken });

               }
                else
                {
                    return NotFound(new { Message = "Invalid Token" });
                }
            }
            catch (Exception ex) {
                return StatusCode(500, new {ex.Message});   
            }
            
        }

        [HttpPost("GenerateOTP")]
        public async Task<IActionResult> GenerateOTP([FromBody] ForgotPasswordDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest(new { Message = "Email is required" });
                }

                var result = await _Authentication.ProcessForgotPassword(request.Email);
                if (result.IsEmailExists)
                {   
                    return Ok(new { Message = "OTP generated successfully", OTP = result.OTP });
                }
                return NotFound(new { Message = "Email not registered" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }



        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOtpDto request)
        {
            try
            {
                var isValid = await _Authentication.VerifyOTP(request.Email, request.Otp);
                if (isValid)
                {
                    return Ok(new { Message = "OTP verified successfully" });
                }
                return BadRequest(new { Message = "Invalid OTP" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }


        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto request)
        {
            try
            {
                if (request.NewPassword != request.ConfirmPassword)
                {
                    return BadRequest(new { Message = "Passwords do not match" });
                }

                var isUpdated = await _Authentication.UpdatePassword(request.Email, request.NewPassword);
                if (isUpdated)
                {
                    return Ok(new { Message = "Password updated successfully" });
                }
                return BadRequest(new { Message = "Failed to update password" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    








}
}
    

