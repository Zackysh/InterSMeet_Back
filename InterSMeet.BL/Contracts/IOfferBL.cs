using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.BLL.Contracts
{
    public interface IOfferBL
    {
        IEnumerable<OfferDTO> FindAll();
        OfferDTO FindById(int offerId);
        IEnumerable<OfferDTO> FindCompanyOffers(string name);
        OfferDTO Create(CreateOfferDTO createOfferDTO, string username);
        OfferDTO Update(UpdateOfferDTO updateOfferDTO, string username, int offerId);
        OfferDTO Delete(int offerId, string username);
    }
}
