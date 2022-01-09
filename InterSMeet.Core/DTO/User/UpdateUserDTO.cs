using InterSMeet.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class UpdateUserDTO
    {
        [MaxLength(40)]
        public string? Username { get; set; }
        [MaxLength(255)]
        public string? Email { get; set; }
        [MaxLength(70)]
        public string? FirstName { get; set; }
        [MaxLength(70)]
        public string? LastName { get; set; }
        public int? ProvinceId { get; set; }
        public string? Location { get; set; }
        public int? LanguageId { get; set; }
    }
}
