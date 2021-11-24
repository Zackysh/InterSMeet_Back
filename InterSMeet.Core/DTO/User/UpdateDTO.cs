﻿using System;
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
        public string? Username { get; set; } = null!;
        [MaxLength(70)]
        public string? FirstName { get; set; } = null!;
        [MaxLength(70)]
        public string? LastName { get; set; } = null!;
        [MaxLength(255)]
        public string? Email { get; set; } = null!;
        public int? LanguageId { get; set; } = null!;
        public int? RoleId { get; set; }
    }
}
