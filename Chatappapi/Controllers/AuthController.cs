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
        
        public AuthController(IAuthentication Authentication,AuthServices AuthServices)
        {
            _Authentication = Authentication;
            _AuthServices = AuthServices;
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

    }
}
