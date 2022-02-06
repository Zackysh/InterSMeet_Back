using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.BLL.Contracts
{
    public interface IUserBL
    {
        
        /// <param name="usertype">Determines if user should be a student or a company</param>
        public AuthenticatedDTO SignIn(SignInDTO signInDTO, string usertype);
        public AuthenticatedDTO RefreshToken(string refreshToken);
        public AuthenticatedDTO StudentSignUp(StudentSignUpDTO signUpDTO);
        public AuthenticatedDTO CompanySignUp(CompanySignUpDTO signUpDTO);
        public IEnumerable<ProvinceDTO> FindAllProvinces();
        public ProvinceDTO FindProvinceById(int provinceId);
        public IEnumerable<LanguageDTO> FindAllLanguages();
        public LanguageDTO FindLanguageById(int languageId);
        public LanguageDTO CreateLanguage(LanguageDTO languageDTO);
        void CheckCredential(string credential);
        void CheckEmail(string email);
        void CheckUsername(string username);
        // @ Email Verification
        void EmailVerification(string verificationCode, string username);
        void SendEmailVerification(string username);
        // @ Restore Password
        void SendRestorePassword(string credential);
        void CheckRestorePassword(string restorePasswordCode, string credential);
        void RestorePassword(string newPassword, string restorePasswordCode, string credential);
    }
}
