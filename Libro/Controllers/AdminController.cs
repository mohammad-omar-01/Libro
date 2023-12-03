using Libro.Data.Models;
using Libro.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "MustBeAdmin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;

        public AdminController(IAdminRepository adminRepository, IUserRepository userRepository)
        {
            this._adminRepository = adminRepository;
            _userRepository = userRepository;
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, string role)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            UserRole userRole;
            Enum.TryParse<UserRole>(role, out userRole);
            user = _adminRepository.AssignUserRole(user, userRole);

            return Ok(user);
        }
    }
}
