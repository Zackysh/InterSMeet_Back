using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    /// <summary>
    /// Entity to save Student Avatar in MySql DB.
    /// This isn't a definitive solution. System based storage is the desired solution.
    /// </summary>
    public class Image
    {
        [Key]
        [Column(name: "image_id")]
        public int ImageId { get; set; }

        [Column(name: "image_title")]
        public string ImageTitle { get; set; } = null!;

        [Column(name: "image_data")]
        [DataType("LONGBLOB")]
        public Byte[] ImageData { get; set; } = null!; // don't simplify to byte[] - EF related issue
    }
}
