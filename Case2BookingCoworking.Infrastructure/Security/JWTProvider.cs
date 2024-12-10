﻿using Case2BookingCoworking.Application.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DODQuiz.Infrastructure.Security
{
    internal class JWTProvider: IJWTProvider
    {
        private readonly IConfiguration configuration;

        public JWTProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var jwtoptions = configuration.GetRequiredSection("jwtoptions");

            List<Claim> claims = [new("UserId", user.Id.ToString())];
            foreach (var role in user.Roles)
            {
                claims.Add(new(ClaimTypes.Role, role.Name));
            }
            var secretKey = jwtoptions.GetRequiredSection("SecretKey").Value;
            var expiresHours = jwtoptions.GetRequiredSection("ExpiresHours").Value;
            var key = Encoding.UTF8.GetBytes(secretKey);
            var hours = Convert.ToInt32(expiresHours);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(hours)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}