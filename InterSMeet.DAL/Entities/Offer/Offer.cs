using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    public class Offer
    {
        [Key, Column("offer_id")]
        public int OfferId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Schedule { get; set; }
        public double Salary { get; set; }
        [ForeignKey("company_id")]
        [Column("company_id")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = null!;
    }
}
