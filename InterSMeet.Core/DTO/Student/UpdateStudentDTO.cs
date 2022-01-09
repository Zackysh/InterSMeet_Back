using InterSMeet.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class UpdateStudentDTO
    {
        public UpdateUserDTO? UpdateUserDto { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public int? DegreeId { get; set; }
        [Range(0, 10)]
        public double? AverageGrades { get; set; }
    }
}
