using Microsoft.IdentityModel.Tokens;
using NG_Express.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NG_Express.Security
{
    public class AuthToken
    {
        private readonly IConfiguration _configuration;
        public AuthToken(IConfiguration config)
        {
            _configuration = config;

        }
        public string GenerateToken(Buyer buyer)
        {
            var SecretKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:SecretKey"));
            var symmetricSecurirtKey = new SymmetricSecurityKey(SecretKey);
            var signingCredentials = new SigningCredentials(symmetricSecurirtKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId",buyer.Id.ToString()),
                new Claim("Name",buyer.FirstName),
            };
            var tokenSchema = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenSchema);
        }
    }
}
