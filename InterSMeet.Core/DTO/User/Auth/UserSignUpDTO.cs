using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    /// <summary>
    /// This DTO is used to validate income Student & Company
    /// registration data (as they share user-data).
    /// </summary>
    public class UserSignUpDTO
    {
        [MaxLength(40)]
        public string Username { get; set; } = null!;

        [MaxLength(255)]
        public string Email { get; set; } = null!;

        [MaxLength(70)]
        public string FirstName { get; set; } = null!;

        [MaxLength(70)]
        public string LastName { get; set; } = null!;

        public int ProvinceId { get; set; }

        public string Location { get; set; } = null!;

        [MaxLength(40)]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        public int LanguageId { get; set; }
    }
}
