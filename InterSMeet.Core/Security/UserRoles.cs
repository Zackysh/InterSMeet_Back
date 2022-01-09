using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.Security
{
    public class UserRoles
    {
        public static int Admin { get; set; } = 1;
        public static int Company { get; set; } = 2;
        public static int Student { get; set; } = 3;
    }
}
