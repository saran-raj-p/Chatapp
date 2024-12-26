using Chatappapi.Interface;
using Chatappapi.Model;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Chatappapi.Controllers
{
    [ApiController]

    [Route("Api/[Controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IForgotPasswordRepository _forgotPasswordRepository;

        public ForgotPasswordController(IForgotPasswordRepository forgotPasswordRepository)
        {
            _forgotPasswordRepository = forgotPasswordRepository;
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotDTo request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.NewPassword) ||
                string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BadRequest(new { Message = "Email and password fields cannot be empty." });
            }

            // Check if email exists in the system
            var isEmailRegistered = await _forgotPasswordRepository.IsEmailRegisteredAsync(request.Email);

            if (!isEmailRegistered)
            {
                return BadRequest(new { Message = "Email is not registered." });
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest(new { Message = "Passwords do not match." });
            }

            if (request.NewPassword.Length < 8)
            {
                return BadRequest(new { Message = "Password must be at least 8 characters long." });
            }

            // Simulate calling the repository to reset the password
            var result = await _forgotPasswordRepository.ResetPasswordAsync(request.ResetToken, request.NewPassword);

            if (!result)
            {
                return BadRequest(new { Message = "Invalid token or unable to update password." });
            }

            return Ok(new { Message = "Password has been successfully updated." });
        }
    }
}