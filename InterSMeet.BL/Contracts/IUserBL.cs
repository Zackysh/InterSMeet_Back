﻿using InterSMeet.Core.DTO;
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
        public AuthenticatedDTO SignIn(SignInDTO signInDTO);
        public AuthenticatedDTO StudentSignUp(StudentSignUpDTO signUpDTO);
        public AuthenticatedDTO CompanySignUp(CompanySignUpDTO signUpDTO);
        public UserDTO FindProfile(string username);
        public IEnumerable<ProvinceDTO> FindAllProvinces();
        public IEnumerable<LanguageDTO> FindAllLanguages();
        public LanguageDTO CreateLanguage(LanguageDTO languageDTO);
        void CheckEmail(string email);
        void CheckUsername(string username);
    }
}
