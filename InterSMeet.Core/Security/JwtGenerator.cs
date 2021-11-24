using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterSMeet.Core.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        internal IConfiguration Configuration;
        public JwtGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string SignAccessToken(UserDTO userDto, Claim? roleClaim = null)
        {
            Claim[]? claims = null;
            if (roleClaim is not null)
                claims = new Claim[] { roleClaim };
            return GetJwtToken(userDto.Username, Configuration["Jwt:AccessSecret"], TimeSpan.FromMinutes(15), claims);
        }
        public string SignRefreshToken(UserDTO userDto)
        {
            return GetJwtToken(userDto.Username, Configuration["Jwt:RefreshSecret"], TimeSpan.FromDays(7));
        }

        /// <summary>
        /// Generate JwtSecurityToken with provided information.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="signingKey"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="expiration"></param>
        /// <param name="additionalClaims"></param>
        /// <returns></returns>
        public string GetJwtToken(
                string username,
                string signingKey,
                TimeSpan expiration,
                Claim[]? additionalClaims = null)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                // this guarantees the token is unique
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (additionalClaims is not null)
            {
                var claimList = new List<Claim>(claims);
                claimList.AddRange(additionalClaims);
                claims = claimList.ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var issuer = Configuration["Jwt:Issuer"];
            var audience = Configuration["Jwt:Audience"];


            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.Add(expiration),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
