using Microsoft.AspNetCore.Mvc;
using Libro.Data.Repos;
using Libro.Data.DTOs;

[ApiController]
[Route("api/[controller]")]
public class Authintication : ControllerBase
{
    private readonly IUserRepository userRepository;

    public Authintication(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost("signup")]
    public IActionResult Signup([FromBody] SignupRequestDTO signupRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid signup request.");
        }

        if (userRepository.IsUsernameTaken(signupRequest.Username))
        {
            return Conflict("Username is already taken.");
        }

        userRepository.RegisterUser(signupRequest.Username, signupRequest.Password);

        return Ok("Signup successful.");
    }
}
