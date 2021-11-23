using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.DAL.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public InterSMeetDbContext _context { get; set; }

        public UserRepository(InterSMeetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> FindAll()
        {
            return _context.Users;
        }

        public User? FindByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User? FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Create(User user)
        {
            var u = _context.Users.Add(user);
            _context.SaveChanges();
            return u.Entity;
        }

        public bool Exists(User user)
        {
            return _context.Users.Any(u => u.Email == user.Email || u.Username == user.Username);
        }
    }
}
