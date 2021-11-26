using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    public class Company
    {
        [Key, Column("company_id"), ForeignKey("User")]
        public int CompanyId { get; set; }
        public string Address { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
