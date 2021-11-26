using InterSMeet.DAL.Entities;
using ObjectDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class StudentDTO
    {
        public int StudentId { get; set; }
        public DateTime BirthDate { get; set; }
        public int DegreeId { get; set; }
        public double AverageGrades { get; set; }
        public int? AvatarId { get; set; }
    }
}
