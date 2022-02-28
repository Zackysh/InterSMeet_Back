using InterSMeet.Core.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterSMeet.Core.Security
{
    public class JwtService : IJwtService
    {
        internal IConfiguration Configuration;
        public JwtService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string SignAccessToken(string username, Claim? roleClaim = null)
        {
            Claim[]? claims = null;
            if (roleClaim is not null)
                claims = new Claim[] { roleClaim };

            return GetJwtToken(username, "4F46925F8983C24B66C7431E5F26C", TimeSpan.FromMinutes(30), claims);
        }
        public string SignRefreshToken(string username)
        {
            return GetJwtToken(username, "CADF18992ADE1D471AA7179ED1B2C", TimeSpan.FromDays(7));
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
                new Claim(ClaimTypes.Name, username),
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

            var token = new JwtSecurityToken(
                issuer: "localhost:back",
                audience: "localhost:front",
                expires: DateTime.UtcNow.Add(expiration),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public ClaimsPrincipal? GetRefreshTokenPrincipal(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CADF18992ADE1D471AA7179ED1B2C")),
                ValidateLifetime = true
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                var exp = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Expiration)?.Value;
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return null;
                return principal;
            }
            catch (SecurityTokenSignatureKeyNotFoundException)
            {
                return null;
            }
        }
    }
}
