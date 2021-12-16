using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class LanguageDTO
    {
        public int LanguageId { get; set; }

        [MaxLength(5)]
        public string Name { get; set; } = null!;
    }
}
