using InterSMeet.Core.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO.Offer
{
    public class OfferPaginationResponseDTO
    {
        public OfferPaginationDTO Pagination { get; set; } = null!;
        public IEnumerable<OfferDTO> Offers = null!;
    }
}
