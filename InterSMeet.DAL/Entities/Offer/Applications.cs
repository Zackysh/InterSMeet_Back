
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IndexAttribute = System.ComponentModel.DataAnnotations.Schema.IndexAttribute;

namespace InterSMeet.DAL.Entities
{
    public enum ApplicationStatus
    {
        Accepted = 1,
        Denied = -1,
        Pending = 0
    }

    [Table("applications")]
    public class Application
    {
        [Key, Column("application_id")]
        public int ApplicationId { get; set; }
        [ForeignKey("student_id")]
        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        [Column("student_id")]
        public int StudentId { get; set; }

        [ForeignKey("offer_id")]
        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        [Column("offer_id")]
        public int OfferId { get; set; }

        [Column("status")]
        public ApplicationStatus Status { get; set; }
        public static IEnumerable<string> ApplicationStatusList()
        {
            return new[]
            {
                ApplicationStatus.Accepted.ToString(),
                ApplicationStatus.Denied.ToString(),
                ApplicationStatus.Pending.ToString()
            };
        }

        public static ApplicationStatus? GetApplicationStatus(string status)
        {
            var asd = ApplicationStatus.Accepted.ToString();
            if (ApplicationStatus.Pending.ToString().Equals(status))
                return ApplicationStatus.Pending;
            if (ApplicationStatus.Denied.ToString().Equals(status))
                return ApplicationStatus.Denied;
            if (ApplicationStatus.Accepted.ToString().Equals(status))
                return ApplicationStatus.Accepted;
            else return null;
        }
    }
}
