using System;
using System.Collections.Generic;
using System.Text;

namespace InterSMeet.Core.Email
{
    public interface IEmailSender
    {
        void SendRestorePassword(string destination, string randomCode, string username);
        void SendEmailVerification(string destination, string randomCode, string username);
        string RandomCode(int length);

    }
}