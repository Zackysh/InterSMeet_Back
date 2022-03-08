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
        internal IOfferRepository OfferRepository;
        internal IUserBL UserBL;
        internal IMapper Mapper;
        internal IAuthBL AuthBL;

        public StudentBL(
            IStudentRepository studentRepository, IAuthBL authBl, IUserRepository userRepository, IOfferRepository offerRepository, IUserBL userBL, IMapper mapper)
        {
            AuthBL = authBl;
            OfferRepository = offerRepository;
            Mapper = mapper;
            StudentRepository = studentRepository;
            UserRepository = userRepository;
            UserBL = userBL;
        }

        // @ Student

        public IEnumerable<StudentDTO> FindAll()
        {
            var stds = StudentRepository.FindAll();
            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(stds);
        }

        public AuthenticatedDTO Update(UpdateStudentDTO updateDto, string username)
        {
            if (updateDto is null || NullValidator.IsNullOrEmpty(updateDto)) throw new BLBadRequestException("You should update at least one field");

            var currentStudent = FindProfile(username); // check if student exists

            // If username is provided and is different from current username, check if it's available
            if (
                updateDto?.UpdateUserDto?.Username is not null
                && !username.Equals(updateDto.UpdateUserDto.Username)
                && FindProfile_(updateDto.UpdateUserDto.Username) is not null
            )
                throw new BLConflictException("Provided username isn't available");

            if (updateDto?.DegreeId is not null) FindDegreeById((int)updateDto.DegreeId);
            if (updateDto?.UpdateUserDto?.LanguageId is not null) UserBL.FindLanguageById((int)updateDto.UpdateUserDto.LanguageId);
            if (updateDto?.UpdateUserDto?.ProvinceId is not null) UserBL.FindProvinceById((int)updateDto.UpdateUserDto.ProvinceId);

            var newStudentData = Mapper.Map<UpdateStudentDTO, Student>(updateDto!);
            var newUserData = updateDto?.UpdateUserDto is null ? null : Mapper.Map<UpdateUserDTO, User>(updateDto.UpdateUserDto);

            if (newUserData is not null)
            {
                newUserData.UserId = currentStudent.StudentId;
                newUserData.EmailVerified = currentStudent.EmailVerified;
                UserRepository.Update(newUserData);
            }
            newStudentData.StudentId = currentStudent.StudentId;
            StudentRepository.Update(newStudentData);

            return AuthBL.SignAuthDTO(FindProfile(currentStudent.StudentId));
        }

        public StudentDTO Delete(int studentId)
        {
            var student = StudentRepository.FindById(studentId);
            if (student is null) throw new BLNotFoundException($"Student not found with ID: {studentId}");

            StudentRepository.Delete(studentId);
            UserRepository.Delete(studentId);

            return Mapper.Map<Student, StudentDTO>(student);
        }

        // @ Avatar

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

        public ImageDTO DownloadAvatarById(int avatarId)
        {
            var avatar = StudentRepository.DownloadAvatar(avatarId);
            if (avatar is null) throw new BLNotFoundException($"Avatar not found with ID {avatarId}");

            return ImageDTO.FromImage(avatar);
        }

        public ImageDTO DownloadAvatar(int imageId)
        {
            var avatar = StudentRepository.DownloadAvatar(imageId);
            if (avatar is null) throw new BLNotFoundException($"Image not found with ID: {imageId}");
            return ImageDTO.FromImage(avatar);
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

        // @ Degree

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

        // @ Family

        public IEnumerable<FamilyDTO> FindAllFamilies()
        {
            var res = StudentRepository.FindAllFamilies();
            return Mapper.Map<IEnumerable<Family>, IEnumerable<FamilyDTO>>(res);
        }

        // @ Level

        public IEnumerable<LevelDTO> FindAllLevels()
        {
            var res = StudentRepository.FindAllLevels();
            return Mapper.Map<IEnumerable<Level>, IEnumerable<LevelDTO>>(res);
        }

        // @ Applications

        public int ApplicationCount(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLNotFoundException($"Student not found");
            var student = StudentRepository.FindById(user.UserId);
            if (student is null) throw new BLNotFoundException($"Student not found");

            return OfferRepository.ApplicationCount(user.UserId);
        }

        // =============================================================================================
        // @ Private Methods
        // =============================================================================================

        private StudentDTO FindProfile(int studentId)
        {
            var student = StudentRepository.FindById(studentId);
            if (student is null) throw new BLConflictException($"It appears that the user isn't linked to a student");

            return Mapper.Map<Student, StudentDTO>(student);
        }

        private StudentDTO FindProfile(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLNotFoundException($"Student not found with Username: {username}");

            var student = StudentRepository.FindById(user.UserId);
            if (student is null) throw new BLConflictException($"It appears that the user isn't linked to a student");

            return Mapper.Map<Student, StudentDTO>(student);
        }

        private Student? FindProfile_(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) return null;

            return StudentRepository.FindById(user.UserId);
        }
    }
}
