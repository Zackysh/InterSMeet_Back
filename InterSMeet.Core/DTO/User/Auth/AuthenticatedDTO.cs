using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class AuthenticatedDTO
    {
        public UserDTO User { get; set; } = null!;
        public JwtSecurityToken AccessToken { get; set; } = null!;
        public JwtSecurityToken RefreshToken { get; set; } = null!;
    }
}
