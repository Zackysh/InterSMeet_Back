using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        [MaxLength(70)]
        public string FirstName { get; set; } = null!;
        [Column("last_name")]
        [MaxLength(70)]
        public string LastName { get; set; } = null!;
        [MaxLength(255)]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        [MaxLength(10)]
        public string Language { get; set; } = null!;
        [Column("role")]
        [ForeignKey("role_id")]
        public int? RoleId { get; set; }
        public UserRole? Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
