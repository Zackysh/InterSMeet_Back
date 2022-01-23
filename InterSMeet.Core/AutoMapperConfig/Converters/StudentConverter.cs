using AutoMapper;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;

namespace InterSMeet.Core.MapperProfiles
{
    public class StudentConverter : ITypeConverter<Student, StudentDTO>
    {
        public StudentDTO Convert(Student source, StudentDTO destination, ResolutionContext context)
        {
            var student = new StudentDTO()    
            {
                StudentId = source.StudentId,
                UserId = source.User.UserId,
                Username = source.User.Username,
                Email = source.User.Email,
                FirstName = source.User.FirstName,
                LastName = source.User.LastName,
                ProvinceId = source.User.ProvinceId,
                Location = source.User.Location,
                LanguageId = source.User.LanguageId,
                RoleId = source.User.RoleId,
                BirthDate = source.BirthDate,
                DegreeId = source.DegreeId,
                AverageGrades = source.AverageGrades,
                AvatarId = source.AvatarId,
            };
            return student;
        }
    }
}