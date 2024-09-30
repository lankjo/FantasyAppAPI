using Microsoft.AspNetCore.Mvc;
using FantasyAppAPI.Services;
using FantasyAppModels;


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
        public ActionResult<AuthResponse> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = _authService.Login(request.Username, request.Password);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

