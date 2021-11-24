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

        // Language FK
        [Column("language_id")]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        public Language Lang { get; set; } = null!;

        // Role FK
        [Column("role_id")]
        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }
}
