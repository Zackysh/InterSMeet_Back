using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    public class Company
    {
        [Key, Column("company_id"), ForeignKey("User")]
        public int CompanyId { get; set; }
        [Column("company_name"), MaxLength(70)]
        public string CompanyName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
