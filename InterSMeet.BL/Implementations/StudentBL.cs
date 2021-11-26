using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using ObjectDesign;

namespace InterSMeet.BLL.Implementations
{
    public class StudentBL : IStudentBL
    {
        internal IStudentRepository StudentRepository;
        internal IUserRepository UserRepository;
        internal IMapper Mapper;
        public StudentBL(
            IStudentRepository studentRepository, IUserRepository userRepository, IMapper mapper)
        {
            Mapper = mapper;
            StudentRepository = studentRepository;
            UserRepository = userRepository;
        }

        public IEnumerable<StudentDTO> FindAll()
        {
            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(StudentRepository.FindAll());
        }

        public StudentDTO FindProfile(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLNotFoundException($"Student not found with Username: {username}");

            var student = StudentRepository.FindById(user.UserId);
            if (student is null) throw new BLConflictException($"It appears that the user isn't linked to a student");

            student.User = user; // EF doesn't handle lazy loading correcly

            return Mapper.Map<Student, StudentDTO>(student);
        }

        public StudentDTO Delete(int studentId)
        {
            var student = StudentRepository.FindById(studentId);
            if (student is null) throw new BLNotFoundException($"Student not found with ID: {studentId}");

            StudentRepository.Delete(studentId);
            UserRepository.Delete(studentId);

            return Mapper.Map<Student, StudentDTO>(student);
        }

        public int UploadAvatar(ImageDTO imgDto, string username)
        {
            Ensure.NotNull(imgDto);
            Ensure.NotNull(username);

            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new Exception("Token identity not found in DB");

            // Check if image already exists - checking bytes
            var image = Mapper.Map<ImageDTO, Image>(imgDto);
            var exists = StudentRepository.Exists(image);

            // If exists, link image with user
            if (exists is not null) return StudentRepository.UploadAvatar(exists.ImageId, user.UserId).ImageId;

            // If not, store and link
            return StudentRepository.UploadAvatar(image, user.UserId).ImageId;
        }

        public ImageDTO DownloadAvatarByStudent(int studentId)
        {
            var student = StudentRepository.FindById(studentId);

            if (student is null) throw new BLNotFoundException($"Student not found with ID: {studentId}");
            if (student.AvatarId is null) throw new BLNotFoundException($"Student with ID {studentId} has no avatar");
            var avatar = StudentRepository.DownloadAvatar((int)student.AvatarId);
            if (avatar is null) throw new BLNotFoundException($"Student with ID {studentId} has no avatar");

            return ImageDTO.FromImage(avatar);
        }

        public ImageDTO DownloadAvatar(int imageId)
        {
            var avatar = StudentRepository.DownloadAvatar(imageId);
            if (avatar is null) throw new BLNotFoundException($"Image not found with ID: {imageId}");
            return ImageDTO.FromImage(avatar);
        }
    }
}
