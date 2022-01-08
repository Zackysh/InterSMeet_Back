using InterSMeet.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class DegreeDTO
    {
        public int DegreeId { get; set; }
        public string Name { get; set; } = null!;
        public int LevelId { get; set; }
        public int FamilyId { get; set; }
    }
}
