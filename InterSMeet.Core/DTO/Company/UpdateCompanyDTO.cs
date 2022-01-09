using InterSMeet.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class UpdateCompanyDTO
    {
        public UpdateUserDTO? UpdateUserDto { get; set; } = null!;
        public DateTime? Address { get; set; }
    }
}
