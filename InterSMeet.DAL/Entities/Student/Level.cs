using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.DAL.Entities
{
    /// <summary>
    /// Level of Degree.
    /// </summary>
    [Index(nameof(Name), IsUnique = true)]
    public class Level
    {
        [Key]
        [Column("level_id")]
        public int LevelId { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
    }
}
