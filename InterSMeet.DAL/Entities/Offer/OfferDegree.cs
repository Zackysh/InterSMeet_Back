
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IndexAttribute = System.ComponentModel.DataAnnotations.Schema.IndexAttribute;

namespace InterSMeet.DAL.Entities
{
    [Table("offer_degree")]
    
    public class OfferDegree
    {
        [Key, Column("offer_degree_id")]
        public int OfferDegreeId { get; set; }
        [ForeignKey("degree_id")]
        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        [Column("degree_id")]
        public int DegreeId { get; set; }
 
        [ForeignKey("offer_id")]
        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        [Column("offer_id")]
        public int OfferId { get; set; }
    }
}
