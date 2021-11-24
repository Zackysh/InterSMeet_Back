using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    [Table("user_roles")]
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
