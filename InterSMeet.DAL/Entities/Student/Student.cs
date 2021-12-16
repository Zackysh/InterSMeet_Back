using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    public class Student
    {
        [Key, Column("student_id"), ForeignKey("User")]
        public int StudentId { get; set; }

        [Column(name: "birthdate")]
        public DateTime BirthDate { get; set; }

        [Column("average_grades")]
        public double AverageGrades { get; set; }

        [ForeignKey("degree_id")]
        [Column("degree_id")]
        public int DegreeId { get; set; }

        [ForeignKey("avatar_id")]
        [Column("avatar_id")]
        public int? AvatarId { get; set; }

        // @ Virtual
        public User User { get; set; } = null!; // don't use virtual on user - fk related issue
        public virtual Degree Degree { get; set; } = null!;
        public virtual Image? Avatar { get; set; }
    }
}
