using Chatappapi.Interface;
using Chatappapi.Model;
using Chatappapi.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _Authentication;
        private readonly AuthServices _AuthServices;
        
        public AuthController(IAuthentication authentication,AuthServices authServices)
        {
            _Authentication = authentication;
            _AuthServices = authServices;
        }
        [HttpPost("UserRegistration")]
        public async Task<IActionResult> Registration(RegisterDto userdata)
        {
            try
            {
                var result = await _Authentication.UserRegister(userdata);
                if (result != null)
                {

                    LoginDTo user = new LoginDTo();
                    user.Email = userdata.Email;
                    user.Password = userdata.Password;
                    await UserLogin(user);
                    return Ok(new {Message="User Registration Sucessful"});
                }
                return BadRequest(new { Message = "User Registration Failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500,new {Message ="Internal Server Error"});
            }
            
        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginDTo userdata)
        {
            var email = userdata.Email;
            var password = userdata.Password;
            var checkIfUserExists = await _Authentication.UserData(userdata);
            
                if (checkIfUserExists == 1)
                {
               var AuthToken = _AuthServices.GenerateToken(userdata);
                return Ok(new {token=AuthToken});
                }
            return BadRequest();
        }

    }
}
