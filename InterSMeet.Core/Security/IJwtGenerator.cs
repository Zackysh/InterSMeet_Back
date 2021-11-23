using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InterSMeet.Core.Security
{
    public interface IJwtGenerator
    {
        public JwtSecurityToken GetJwtToken(
                string username,
                string signingKey,
                TimeSpan expiration,
                Claim[]? additionalClaims = null);
    }
}