using InterSMeet.Core.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO.Offer
{
    public class OfferPaginationOptionsDTO
    {
        [Range(0, int.MaxValue)]
        public int Page { get; set; }
        [Range(1, int.MaxValue)]
        public int Size { get; set; }
        public bool SkipExpired { get; set; } = true;
        public bool PrivateData { get; set; } = false;
        public string? Search { get; set; }
        public int? CompanyId { get; set; }
        public int? DegreeId { get; set; }
        public int? FamilyId { get; set; }
        public int? LevelId { get; set; }
        // @ Salary
        [Range(0, int.MaxValue)]
        public double? Min { get; set; }
        [Range(0, int.MaxValue)]
        public double? Max { get; set; }
    }
}
