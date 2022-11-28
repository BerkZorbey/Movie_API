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
            var signingCredentials = GetSecurityKey();
            var tokenOptions = GenerateTokenOptions(signingCredentials, expiration);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(tokenOptions);
            token.RefreshToken = Guid.NewGuid().ToString();
            token.RefreshTokenExpiration = expiration.AddHours(1);

            return token;
        }
        private SigningCredentials GetSecurityKey()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtToken:Key"]);
            var securityKey = new SymmetricSecurityKey(key);
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,DateTime expiration)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["JwtToken:Issuer"],
                audience: _configuration["JwtToken:Audience"],
                notBefore: DateTime.Now,
                expires: expiration,
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
