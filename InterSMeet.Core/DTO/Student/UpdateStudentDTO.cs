using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO
{
    public class UpdateStudentDTO
    {
        public UpdateUserDTO? UpdateUserDto { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public int? DegreeId { get; set; }
        [Range(-1, 10)]
        public double? AverageGrades { get; set; }
    }
}
