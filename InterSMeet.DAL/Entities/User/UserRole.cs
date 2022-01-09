using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace InterSMeet.DAL.Entities
{
    [Table("user_roles")]
    [Index(nameof(Name), IsUnique = true)]
    public class UserRole
    {
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("name")]
        [MaxLength(40)]
        public string Name { get; set; } = null!;
    }
}
