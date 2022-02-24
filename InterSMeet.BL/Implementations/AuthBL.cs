using AutoMapper;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.Security;
using InterSMeet.DAL.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace InterSMeet.BLL.Implementations
{
    public class AuthBL : IAuthBL
    {

        internal IUserRepository UserRepository;
        internal IJwtService JwtService;

        public AuthBL(
            IUserRepository userRepository, IMapper mapper,
            IJwtService jwtGenerator, IConfiguration configuration)
        {
            JwtService = jwtGenerator;
            UserRepository = userRepository;
        }

        public AuthenticatedDTO SignAuthDTO(UserDTO userDto, string? refreshToken = null)
        {
            Claim? roleClaim = null;
            if (userDto.RoleId is not null)
            {
                var role = UserRepository.FindRoleById((int)userDto.RoleId);
                if (role is null) throw new NullReferenceException(nameof(role));
                roleClaim = new Claim(ClaimTypes.Role, role.Name);
            }

            return new()
            {
                User = userDto,
                AccessToken = JwtService.SignAccessToken(userDto.Username, roleClaim),
                RefreshToken = refreshToken ?? JwtService.SignRefreshToken(userDto.Username)
            };
        }
    }
}