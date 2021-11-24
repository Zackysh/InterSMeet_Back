using InterSMeet.Core.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InterSMeet.Core.Security
{
    public interface IJwtGenerator
    {
        public string GetJwtToken(
                string username,
                string signingKey,
                TimeSpan expiration,
                Claim[]? additionalClaims = null);

        public string SignAccessToken(UserDTO userDto, Claim? roleClaim = null);
        public string SignRefreshToken(UserDTO userDto);
    }
}