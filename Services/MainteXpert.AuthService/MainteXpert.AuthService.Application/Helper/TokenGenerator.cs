using Authentication.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Application.Helper
{
    public interface ITokenGenerator
    {
        TokenModel CreateAccessToken(string userId, bool isAuthenticated = true);
    }


    public class TokenGenerator : ITokenGenerator
    {
        public readonly IConfiguration _configuration;

        public TokenGenerator( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenModel CreateAccessToken(string userId, bool isAuthenticated = true)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]));
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };
            DateTime now = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                     issuer: _configuration["JWT:Issuer"],
                     audience: _configuration["JWT:Audience"],
                     claims: new List<Claim> {
                         new Claim(ClaimTypes.Name, userId),
                         new Claim(ClaimTypes.Authentication, isAuthenticated.ToString())
                     },
                     notBefore: now,
                     expires: now.Add(TimeSpan.FromHours(Convert.ToInt32(_configuration["JWT:Expires"]))),
                     signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                 );
            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                ExpireTime = TimeSpan.FromHours(Convert.ToInt32(_configuration["JWT:Expires"])).TotalSeconds
            };
        }

    }



}
