using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using FantasyAppAPI.Models;

namespace FantasyAppAPI.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly List<User> _users = new List<User>(); 

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string Authenticate(LoginRequest loginRequest)
        {
            var user = _users.FirstOrDefault(u => u.Username == loginRequest.Username);
            if (user == null || !VerifyPassword(loginRequest.Password, user.PasswordHash))
                return null;

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisisatestthisIsOnlyATest"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return password == passwordHash;
        }
    }
}
