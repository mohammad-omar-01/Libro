using Libro.Data.DTOs;
using Libro.Data.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class Authintication : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration _configratuion;

    public Authintication(IUserRepository userRepository, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        _configratuion = configuration;
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

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var user = userRepository.AuthenticateUser(loginRequest.Username, loginRequest.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configratuion["Authentication:SecretKey"])
        );
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("ID", user.ID.ToString()));
        claimsForToken.Add(new Claim("Name", user.Username));
        claimsForToken.Add(new Claim("Role", user.Role.ToString()));

        var jwtSecurityToken = new JwtSecurityToken(
            _configratuion["Authentication:Issuer"],
            _configratuion["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            signingCredentials
        );

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Ok(tokenToReturn);
    }
}
