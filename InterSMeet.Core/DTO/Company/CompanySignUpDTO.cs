using InterSMeet.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO
{
    public class CompanySignUpDTO
    {
        public UserSignUpDTO UserSignUpDto { get; set; } = null!;
        [MaxLength(70)]
        public string CompanyName { get; set; } = null!;
        public string Address { get; set; } = null!;
        
        public static Company ToCompany(CompanySignUpDTO signUpDto, User user)
        {
            Company company = new();
            company.User = user;
            company.Address = signUpDto.Address;
            company.CompanyName = signUpDto.CompanyName;
            return company;
        }
    }
}
