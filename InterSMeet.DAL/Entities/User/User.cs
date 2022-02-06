using InterSMeet.DAL.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace InterSMeet.DAL.Entities
{
    /// <summary>
    /// Cornerstone for Student & Company.
    /// Both models inherite from User, to centralize and simplify
    /// authentication.
    /// </summary>
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity

    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        [MaxLength(255)]
        public string Email { get; set; } = null!;

        [Column("first_name")]
        [MaxLength(70)]
        public string FirstName { get; set; } = null!;

        [Column("last_name")]
        [MaxLength(70)]
        public string LastName { get; set; } = null!;

        [ForeignKey("province_id")]
        [Column("province_id")]
        public int ProvinceId { get; set; }

        [Column("location")]
        public string Location { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [ForeignKey("language_id")]
        [Column("language_id")]
        public int LanguageId { get; set; }

        [ForeignKey("role_id")]
        [Column("role_id")]
        public int? RoleId { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("email_verification")]
        public string? EmailVerificationCode { get; set; }

        [Column("forgot_password")]
        public string? ForgotPasswordCode { get; set; }
        [Column("email_verified")]
        public bool EmailVerified { get; set; } = false;

        // @ Virtual
        public virtual UserRole? Role { get; set; }
        public virtual Language Language { get; set; } = null!;
        public virtual Province Province { get; set; } = null!;
    }
}
