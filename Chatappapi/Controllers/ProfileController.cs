using Chatappapi.Interface;
using Chatappapi.Model;
using Chatappapi.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ProfileCloudService _profileCloudService;

        public ProfileController(IProfileRepository profileRepository, ProfileCloudService profileCloudService ) {
            _profileRepository = profileRepository;
            _profileCloudService = profileCloudService; 
        }

        [HttpGet("getprofile")] 
        public async Task<IActionResult> GetProfile([FromQuery]Guid id )
        {
            var profileData = await _profileRepository.GetProfileData(id);            
            return Ok(new { Data = profileData }); 
        }

        [HttpPost("updateProfile")]
        public async Task<IActionResult> UpdateProfile(IFormFile image, [FromForm] updateProfile model)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest(new { message = "No image file uploaded." });
            }
            try
            {
                var profileUrl = await _profileCloudService.uploadImageToCloud(image);

                await _profileRepository.UpdateProfileData(model, profileUrl);
                return Ok(new { message = "Profile updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the profile. Please try again later." });
            }

        }
    }
}
