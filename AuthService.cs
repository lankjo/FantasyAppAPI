using FantasyAppModels;
using FantasyAppData;
using FantasyAppData.Repositories;


namespace FantasyAppAPI.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthResponse Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && user.ValidatePassword(password))
            {
                return new AuthResponse
                {
                    Token = GenerateToken(user),
                    Expiration = DateTime.UtcNow.AddHours(1)
                };
            }
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        private string GenerateToken(User user)
        {
            // Token generation logic (JWT, etc.)
            return "generated_token";
        }
    }
}
