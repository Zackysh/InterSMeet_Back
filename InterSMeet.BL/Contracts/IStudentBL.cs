using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.BLL.Contracts
{
    public interface IStudentBL
    {
        IEnumerable<StudentDTO> FindAll();
        StudentDTO FindProfile(string username);
        StudentDTO Delete(int studentId);
        int UploadAvatar(ImageDTO img, string username);
        ImageDTO DownloadAvatarByStudent(int studentId);
        ImageDTO DownloadAvatarByStudent(string username);
        ImageDTO DownloadAvatar(int imageId);
        IEnumerable<DegreeDTO> FindAllDegrees();
        DegreeDTO FindDegreeById(int degreeId);
    }
}
