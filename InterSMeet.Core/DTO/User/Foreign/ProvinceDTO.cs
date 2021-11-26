using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class ProvinceDTO
    {
        public int ProvinceId { get; set; }
        public string Name { get; set; } = null!;
    }
}
