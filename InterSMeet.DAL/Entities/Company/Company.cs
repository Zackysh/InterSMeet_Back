using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    public class Company
    {
        [Key, Column("company_id"), ForeignKey("User")]
        public int CompanyId { get; set; }
        [Column("stripe_id")]
        public string? StripeId { get; set; }
        [Column("company_name"), MaxLength(70)]
        public string CompanyName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Description { get; set; }
        public string? Web { get; set; }
        public string? BackgroundUrl { get; set; }
        public string? LogoUrl { get; set; }
        public User User { get; set; } = null!;
    }
}
