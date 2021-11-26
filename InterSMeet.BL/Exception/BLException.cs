using System;
using System.Collections.Generic;
using System.Text;

namespace InterSMeet.BL.Exception
{
    /// <summary>
    /// Abstract class to centralize business exceptions
    /// produced on BL Layer.
    /// </summary>
    public abstract class BLException : System.Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Error message</param>
        public BLException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Should be used to produce error code 404 (not found).
    /// </summary>
    public class BLNotFoundException : BLException
    {
        public BLNotFoundException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Should be used to produce error code 400 (bad request).
    /// </summary>
    public class BLBadRequestException : BLException
    {
        public BLBadRequestException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Should be used to produce error code 409 (conflict).
    /// </summary>
    public class BLConflictException : BLException
    {
        public BLConflictException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Should be used to produce error code 401 (unauthorized).
    /// </summary>
    public class BLUnauthorizedException : BLException
    {
        public BLUnauthorizedException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Should be used to produce error code 403 (forbidden).
    /// </summary>
    public class BLForbiddenException : BLException
    {
        public BLForbiddenException(string message) : base(message)
        {
        }
    }
}
