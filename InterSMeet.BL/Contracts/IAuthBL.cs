
using InterSMeet.Core.DTO;

namespace InterSMeet.BLL.Contracts
{
    public interface IAuthBL
    {
        public AuthenticatedDTO SignAuthDTO(UserDTO userDto, string? refreshToken = null);
    }
}
