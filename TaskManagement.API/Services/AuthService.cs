using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.API.Interfaces;

namespace TaskManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthService(IConfiguration config)
        {
            _key = config["JwtSettings:Key"];
            _issuer = config["JwtSettings:Issuer"];
            _audience = config["JwtSettings:Audience"];
        }

        public string GenerateToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
