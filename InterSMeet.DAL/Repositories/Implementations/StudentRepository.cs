using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.DAL.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        public InterSMeetDbContext _context { get; set; }

        public StudentRepository(InterSMeetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> FindAll()
        {
            var students = _context.Students
            .Join(
                _context.Users,
                s => s.StudentId,
                u => u.UserId,
                (student, user) => new { student, user }
            );

            foreach (var std in students)
                std.student.User = std.user;

            return students.Select(full => full.student);
        }

        // @ CRUD

        public Student? FindById(int studentId)
        {
            var std = _context.Students.Find(studentId);
            if (std == null) return std;
            std.User = _context.Users.Find(studentId)!;
            return std;
        }

        public Student Create(Student student)
        {
            var change = _context.Students.Add(student);
            _context.SaveChanges();
            return FindById(change.Entity.StudentId)!;
        }

        public Student? Update(Student student)
        {
            var existing = _context.Students.Find(student.StudentId);
            if (existing is not null)
            {
                existing = EntityPropertyMapper.InjectNonNull(existing, student);
                _context.SaveChanges();
                return FindById(existing.StudentId)!;
            }
            return null;
        }

        public Student? Delete(int studentId)
        {
            var student = FindById(studentId);
            if (student is not null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return student;
            }
            else return null;
        }

        public Image? Exists(Image img)
        {
            return _context.Images.FirstOrDefault(i => i.ImageData == img.ImageData);
        }

        // @ Foreign

        // ------------------------------------------------------------------------------
        // @ Degree
        // -----------------------------------------------------------------------------

        public IEnumerable<Degree> FindAllDegrees()
        {
            return _context.Degrees;
        }

        public Degree? FindDegreeById(int degreeId)
        {
            return _context.Degrees.Find(degreeId);
        }

        // ------------------------------------------------------------------------------
        // @ Avatar (Image)
        // -----------------------------------------------------------------------------

        /// <summary>
        /// Store new avatar and associate it with a student.
        /// </summary>
        /// <returns>Stored avatar</returns>
        public Image UploadAvatar(Image image, int studentId)
        {
            var change = _context.Images.Add(image);

            var student = _context.Students.Find(studentId);
            if (student is not null)
                student.Avatar = change.Entity;

            _context.SaveChanges();
            return change.Entity;
        }

        /// <summary>
        /// Associate stored avatar with a student.
        /// </summary>
        /// <returns>Asociated avatar</returns>
        /// <exception cref="NullReferenceException"></exception>
        public Image UploadAvatar(int imageId, int studentId)
        {
            var image = _context.Images.Find(imageId);
            var student = _context.Students.Find(studentId);

            if (student is not null && image is not null)
                student.Avatar = image;

            _context.SaveChanges();
            return image ?? throw new NullReferenceException(nameof(image));
        }

        public Image? DownloadAvatarByStudent(int studentId)
        {
            return _context.Students.Find(studentId)?.Avatar;
        }

        public Image? DownloadAvatar(int imageId)
        {
            return _context.Images.Find(imageId);
        }
    }
}