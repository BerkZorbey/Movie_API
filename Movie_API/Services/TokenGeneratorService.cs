using Microsoft.IdentityModel.Tokens;
using Movie_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Movie_API.Services
{
    public class TokenGeneratorService
    {
        private readonly IConfiguration _configuration;
        public TokenGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Token GenerateToken()
        {
            Token token = new();
            var expiration = DateTime.Now.AddHours(1);
            token.TokenExpiration = expiration;
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                notBefore: DateTime.Now,
                expires: expiration,
                signingCredentials: signingCredentials
            );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            token.RefreshToken = Guid.NewGuid().ToString();
            token.RefreshTokenExpiration = expiration.AddHours(1);

            return token;
        }
    }
}
