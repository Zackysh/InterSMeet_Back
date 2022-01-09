using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace InterSMeet.DAL.Entities
{
    /// <summary>
    /// Family of Degree.
    /// </summary>
    [Index(nameof(Name), IsUnique = true)]
    public class Family
    {
        [Key]
        [Column("family_id")]
        public int FamilyId { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
    }
}
