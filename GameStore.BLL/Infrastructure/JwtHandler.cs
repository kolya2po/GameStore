using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.BLL.Infrastructure
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;

        public JwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetJwtToken(IEnumerable<Claim> claims)
        {
            var signingCredentials = GetSigningCredentials();
            var configuredToken = GenerateToken(signingCredentials, claims);

            var token = new JwtSecurityTokenHandler().WriteToken(configuredToken);

            return token;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:secret"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateToken(SigningCredentials credentials, 
            IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _configuration["JWT:validIssuer"],
                audience: _configuration["JWT:validAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(
                    double.Parse(_configuration["JWT:lifeTimeInHours"])),
                signingCredentials: credentials);
        }
    }
}
