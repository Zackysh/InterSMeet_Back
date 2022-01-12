using InterSMeet.Core.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO.Offer
{
    public class OfferPaginationDTO
    {
        [Range(0, int.MaxValue)]
        public int Page { get; set; }
        [Range(1, int.MaxValue)]
        public int Size { get; set; }
        public string? Search { get; set; }

        public int? CompanyId;

        [Range(0, int.MaxValue)]
        public double? Min { get; set; }

        [Range(0, int.MaxValue)]
        public double? Max { get; set; }
    }
}
