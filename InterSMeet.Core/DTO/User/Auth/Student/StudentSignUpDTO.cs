using InterSMeet.DAL.Entities;


namespace InterSMeet.Core.DTO
{
    public class StudentSignUpDTO
    {
        public UserSignUpDTO UserSignUpDto { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int DegreeId { get; set; }
        public double AverageGrades { get; set; }

        public static Student ToStudent(StudentSignUpDTO signUpDto, Degree degree, User user)
        {
            Student student = new();
            student.BirthDate = signUpDto.BirthDate;
            student.AverageGrades = signUpDto.AverageGrades;
            student.Degree = degree;
            student.User = user;
            return student;
        }
    }
}
