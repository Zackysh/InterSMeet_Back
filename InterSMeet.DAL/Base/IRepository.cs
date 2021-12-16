using InterSMeet.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.DAL.Base
{
    public interface IRepository<T>
    {
        public InterSMeetDbContext _context { get; set; }
    }
}
