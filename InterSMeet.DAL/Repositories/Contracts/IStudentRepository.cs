using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Base;

namespace InterSMeet.DAL.Repositories.Contracts
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Student> FindAll();
        Student? FindById(int studentId);
        Student Create(Student student);
        Student? Delete(int studentId);
        Image? Exists(Image img);
        Image UploadAvatar(Image img, int studentId);
        Image UploadAvatar(int imageId, int studentId);
        Image? DownloadAvatarByStudent(int studentId);
        Image? DownloadAvatar(int imageId);
        Degree? FindDegreeById(int degreeId);
        IEnumerable<Degree> FindAllDegrees();
    }
}
