using System;
using System.Collections.Generic;
using System.Text;
using InterSMeet.DAL.Entities;

namespace InterSMeet.DAL.Repositories.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> FindAll();
        User? FindByUsername(string username);
        User? FindByEmail(string email);
        User Create(User user);
        bool Exists(User user);
    }
}
