using InterSMeet.Core.DTO;

namespace InterSMeet.BLL.Contracts
{
    public interface IStudentBL
    {
        IEnumerable<StudentDTO> FindAll();
        AuthenticatedDTO Update(UpdateStudentDTO updateDto, string username);
        StudentDTO Delete(int studentId);
        int UploadAvatar(ImageDTO img, string username);
        int ApplicationCount(string username);
        ImageDTO DownloadAvatarByStudent(string username);
        ImageDTO DownloadAvatarById(int avatarId);
        IEnumerable<DegreeDTO> FindAllDegrees();
        IEnumerable<FamilyDTO> FindAllFamilies();
        IEnumerable<LevelDTO> FindAllLevels();
        DegreeDTO FindDegreeById(int degreeId);
    }
}
