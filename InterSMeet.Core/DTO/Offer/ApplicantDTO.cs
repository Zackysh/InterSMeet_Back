using InterSMeet.DAL.Entities;

namespace InterSMeet.Core.DTO.Offer
{
    public class ApplicantDTO : StudentDTO
    {
        public string Status { get; set; } = null!;

        public static ApplicantDTO FromStudentDTO(StudentDTO offerDto, ApplicationStatus status)
        {
            return new()
            {
                UserId = offerDto.UserId,
                Username = offerDto.Username,
                Email = offerDto.Email,
                FirstName = offerDto.FirstName,
                LastName = offerDto.LastName,
                ProvinceId = offerDto.ProvinceId,
                Location = offerDto.Location,
                LanguageId = offerDto.LanguageId,
                RoleId = offerDto.RoleId,
                StudentId = offerDto.StudentId,
                BirthDate = offerDto.BirthDate,
                DegreeId = offerDto.DegreeId,
                AverageGrades = offerDto.AverageGrades,
                AvatarId = offerDto.AvatarId,
                Status = status.ToString(),
            };
        }
    }
}
