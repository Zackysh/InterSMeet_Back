using InterSMeet.DAL.Entities;


namespace InterSMeet.Core.DTO
{
    public class CompanySignUpDTO
    {
        public UserSignUpDTO UserSignUpDto { get; set; } = null!;
        public string Address { get; set; } = null!;
        
        public static Company ToCompany(CompanySignUpDTO signUpDto, User user)
        {
            Company company = new();
            company.User = user;
            company.Address = signUpDto.Address;
            return company;
        }
    }
}
