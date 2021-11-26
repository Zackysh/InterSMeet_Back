using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Entity which represent a school grade (título académico).
/// Its compound of a name, family and level (grado medio, superior...).
/// </summary>
namespace InterSMeet.DAL.Entities
{
    // Multi-Key unique-constraint not supported by EF - Dont forget to validate name+level+family unique constraint
    public class Degree
    {
        [Key]
        [Column("degree_id")]
        public int DegreeId { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [ForeignKey("level_id")]
        [Column("level_id")]
        public int LevelId { get; set; }

        [ForeignKey("family_id")]
        [Column("family_id")]
        public int FamilyId { get; set; }

        // @ Virtual
        public virtual Level Level { get; set; } = null!;
        public virtual Family Family { get; set; } = null!;
    }
}
