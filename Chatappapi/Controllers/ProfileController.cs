using Chatappapi.Interface;
using Chatappapi.Model;
using Chatappapi.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            try
            {
                var profileData = await _profileRepository.GetProfileData(id);
                if (profileData == null)
                {
                    return NotFound(new { message = "Profile Data not found"});
                }
                return Ok(new { Data = profileData });

            }
            catch(Exception ex) 
            {
                return StatusCode(500,new {message = "an error occur while fetching profile.",ex.Message});
            }
        }

        [HttpPost("updateProfile")]
        public async Task<IActionResult> UpdateProfile( [FromForm] updateProfile model)
        {
            try
            {
                string? profileUrl = null;

                if (model.Image != null) 
                {
                     profileUrl = await _profileCloudService.uploadImageToCloud(model.Image);
                }
                    
                var result = await _profileRepository.UpdateProfileData(model, profileUrl);

                if (result != 1)
                {
                    return BadRequest(new { message = "Profile update Failed. Try agin" });
                }

                return Ok(new { message = "Profile updated successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the profile. Please try again later.",ex.Message });
            }

        }
    }
}
