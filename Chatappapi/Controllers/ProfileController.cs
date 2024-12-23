using Chatappapi.Interface;
using Chatappapi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository) {
            _profileRepository = profileRepository;
        }

        [HttpGet("getprofile")] 
        public async Task<IActionResult> GetProfile([FromQuery]Guid id )
        {
            var profileData = await _profileRepository.GetProfileData(id);            
            return Ok(new { Data = profileData }); 
        }
    }
}
