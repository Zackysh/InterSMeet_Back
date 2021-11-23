using System;
using System.Collections.Generic;
using System.Text;

namespace InterSMeet.BL.Exception
{
    public abstract class BLException : System.Exception
    {
        public BLException(string message) : base(message)
        {
        }
    }

    public class BLNotFoundException : BLException
    {
        public BLNotFoundException(string message) : base(message)
        {
        }
    }

    public class BLConflictException : BLException
    {
        public BLConflictException(string message) : base(message)
        {
        }
    }

    public class BLUnauthorizedException : BLException
    {
        public BLUnauthorizedException(string message) : base(message)
        {
        }
    }
}
