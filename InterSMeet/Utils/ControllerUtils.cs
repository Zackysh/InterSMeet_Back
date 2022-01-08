using InterSMeet.BL.Exception;
using System.Security.Claims;

namespace InterSMeet.ApiRest.Utils
{
    public class ControllerUtils
    {

        public static string GetUserIdentity(HttpContext context)
        {
            if (context.User.Identity is not ClaimsIdentity claims) throw new BLUnauthorizedException("Invalid token");
            var authUsernameClaim = claims.FindFirst(ClaimTypes.Name);
            if (authUsernameClaim is null) throw new BLUnauthorizedException("Invalid token");
            var username = authUsernameClaim.Value;
            return username;
        }


    }
}
