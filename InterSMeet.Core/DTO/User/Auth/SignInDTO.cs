using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class SignInDTO
    {
        public string Credential { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
