using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class UserRoleDTO
    {
        public int RoleId { get; set; }

        [MaxLength(40)]
        public string Name { get; set; } = null!;
    }
}
