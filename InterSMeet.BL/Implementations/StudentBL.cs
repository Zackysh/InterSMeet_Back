using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Validators;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.BLL.Implementations
{
    public class StudentBL : IStudentBL
    {
        internal IStudentRepository StudentRepository;
        internal IUserRepository UserRepository;
        internal IUserBL UserBL;

        internal IMapper Mapper;
        public StudentBL(
            IStudentRepository studentRepository, IUserRepository userRepository, IUserBL userBL, IMapper mapper)
        {
            Mapper = mapper;
            StudentRepository = studentRepository;
            UserRepository = userRepository;
            UserBL = userBL;
        }

        public IEnumerable<StudentDTO> FindAll()
        {
            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(StudentRepository.FindAll());
        }

        public StudentDTO Update(UpdateStudentDTO updateDTO, string username)
        {
            if (NullValidator.IsNullOrEmpty(updateDTO)) throw new BLBadRequestException("You should update at least one field");

            FindProfile(username); // check if student exists

            if(updateDTO.DegreeId is not null) FindDegreeById((int)updateDTO.DegreeId);
            if (updateDTO?.UpdateUserDto?.LanguageId is not null) UserBL.FindLanguageById((int)updateDTO.UpdateUserDto.LanguageId);
            if (updateDTO?.UpdateUserDto?.ProvinceId is not null) UserBL.FindProvinceById((int)updateDTO.UpdateUserDto.ProvinceId);

            StudentRepository.Update(Mapper.Map<UpdateStudentDTO, Student>(updateDTO!));
            return FindProfile(username);
        }

        public StudentDTO FindProfile(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLNotFoundException($"Student not found with Username: {username}");

            var student = StudentRepository.FindById(user.UserId);
            if (student is null) throw new BLConflictException($"It appears that the user isn't linked to a student");

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
            if (imgDto == null) throw new();
            if (username == null) throw new();

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

        public IEnumerable<DegreeDTO> FindAllDegrees()
        {
            var res = StudentRepository.FindAllDegrees();
            return Mapper.Map<IEnumerable<Degree>, IEnumerable<DegreeDTO>>(res);
        }

        public DegreeDTO FindDegreeById(int degreeId)
        {
            var degree = StudentRepository.FindDegreeById(degreeId);
            if (degree is null) throw new BLNotFoundException("Specified degree doesn't exists");
            return Mapper.Map<Degree, DegreeDTO>(degree);
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

        public ImageDTO DownloadAvatarByStudent(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLNotFoundException($"Student not found");
            var student = StudentRepository.FindById(user.UserId);
            if (student is null) throw new BLNotFoundException($"Student not found");
            if (student.AvatarId is null) throw new BLNotFoundException($"Student with ID {student.StudentId} has no avatar");
            var avatar = StudentRepository.DownloadAvatar((int)student.AvatarId);
            if (avatar is null) throw new BLNotFoundException($"Student with ID {student.StudentId} has no avatar");

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
