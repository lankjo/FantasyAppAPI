using Microsoft.AspNetCore.Mvc;
using FantasyAppAPI.Models;
using FantasyAppAPI.Services;

namespace FantasyAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var token = _authService.Authenticate(loginRequest);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}
