using Libro.Data.DTOs;
using Libro.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatronController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public PatronController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("Profile")]
        [Authorize]
        public ActionResult<PatronProfileDTO> GetPatronProfile()
        {
            var userIdClaim = User.FindFirst("ID");
            if (userIdClaim == null)
            {
                return BadRequest("User ID claim not found in the token.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var patronProfile = _userRepository.GetPatronProfileById(userId);

            return Ok(patronProfile);
        }
    }
}
