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
    [Index(nameof(Name), IsUnique = true)]
    public class Province
    {
        [Key]
        [Column(name: "province_id")]
        public int ProvinceId { get; set; }

        [Column(name: "name")]
        public string Name { get; set; } = null!;
    }
}
