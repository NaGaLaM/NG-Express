using Microsoft.IdentityModel.Tokens;
using NG_Express.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NG_Express.Security
{
    public class AuthToken
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string SecretKey;
        public AuthToken(IConfiguration config)
        {
            _issuer = "Issuer";
            _audience = "Audience";
            SecretKey = config.GetValue<string>("JWT:SecretKey") ?? string.Empty;

        }
        public string GenerateToken<T>(T buyer)
        {
            var Key = Encoding.UTF8.GetBytes(SecretKey);
            var symmetricSecurirtKey = new SymmetricSecurityKey(Key);
            var signingCredentials = new SigningCredentials(symmetricSecurirtKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId",(buyer as dynamic).Id.ToString()),
                new Claim("Name",(buyer as dynamic).FirstName),
                new Claim(ClaimTypes.Role,"Buyer")
            };
            var tokenSchema = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenSchema);
        }
        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromDays(30),
                    RoleClaimType = ClaimTypes.Role
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var type = principal.FindFirst(c => c.Type == ClaimTypes.Role);
                Console.WriteLine(type.Value);
                return principal;
            }
            catch 
            {
                return null;
            }
        }
    }
}
