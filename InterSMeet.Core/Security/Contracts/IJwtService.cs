using InterSMeet.Core.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InterSMeet.Core.Security
{
    public interface IJwtService
    {
        public string GetJwtToken(
                string username,
                string signingKey,
                TimeSpan expiration,
                Claim[]? additionalClaims = null);
        public ClaimsPrincipal? GetRefreshTokenPrincipal(string token);
        public string SignAccessToken(string username, Claim? roleClaim = null);
        public string SignRefreshToken(string username);
    }
}