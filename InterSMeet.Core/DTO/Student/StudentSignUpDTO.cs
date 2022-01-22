using InterSMeet.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO
{
    public class StudentSignUpDTO
    {
        public UserSignUpDTO UserSignUpDto { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int DegreeId { get; set; }
        [Range(0,10)]
        public double AverageGrades { get; set; }

        public static Student ToStudent(StudentSignUpDTO signUpDto, User user)
        {
            Student student = new();
            student.StudentId = user.UserId;
            student.User = user;
            student.BirthDate = signUpDto.BirthDate;
            student.AverageGrades = signUpDto.AverageGrades;
            student.DegreeId = signUpDto.DegreeId;
            return student;
        }
    }
}
