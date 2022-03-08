using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace InterSMeet.Core.Email
{
    public class EmailSender : IEmailSender
    {

        public void SendEmailVerification(string destination, string randomCode, string username)
        {
            var fromAddress = new MailAddress("intersmeet@gmail.com", "IntersMeet");
            var toAddress = new MailAddress(destination, "To Name");
            //const string fromPassword = "^7X3M0NR0*9u8&YmN2CRBbfAgeY";
            const string fromPassword = "vgdqljaaihououaj";
            const string subject = "Email verification";
            string body = $"<td class=\"m_-2717101839704038959mpy-35 m_-2717101839704038959mpx-15\" bgcolor=\"#212429\" style=\"padding: 80px\" > <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tbody> <tr> <td> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tbody> <tr> <td style=\" font-size: 36px; line-height: 42px; font-family: Arial, sans-serif, 'Motiva Sans'; text-align: left; padding-bottom: 30px; color: #bfbfbf; font-weight: bold; \" > <span style=\"color: #77b9ee\">Welcome {username}</span> </td> </tr> </tbody> </table> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#17191c\" > <tbody> <tr> <td style=\" padding-top: 30px; padding-bottom: 30px; padding-left: 56px; padding-right: 56px; \" > <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" > <tbody> <tr> <td style=\" font-size: 18px; line-height: 25px; font-family: Arial, sans-serif, 'Motiva Sans'; color: #8f98a0; text-align: center; \" > Email verification code </td> </tr> <tr> <td style=\"padding-bottom: 16px\"></td> </tr> <tr> <td style=\" font-size: 48px; line-height: 52px; font-family: Arial, sans-serif, 'Motiva Sans'; color: #3a9aed; font-weight: bold; text-align: center; \" > {randomCode} </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            smtp.Send(message);
        }
        public void SendRestorePassword(string destination, string randomCode, string username)
        {
            var fromAddress = new MailAddress("intersmeet@gmail.com", "IntersMeet");
            var toAddress = new MailAddress(destination, "To Name");
            const string fromPassword = "vgdqljaaihououaj";
            const string subject = "Restore password";
            string body = $"<td class=\"m_-2717101839704038959mpy-35 m_-2717101839704038959mpx-15\" bgcolor=\"#212429\" style=\"padding: 80px\" > <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tbody> <tr> <td> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tbody> <tr> <td style=\" font-size: 36px; line-height: 42px; font-family: Arial, sans-serif, 'Motiva Sans'; text-align: left; padding-bottom: 30px; color: #bfbfbf; font-weight: bold; \" > <span style=\"color: #77b9ee\">{username}</span> </td> </tr> </tbody> </table> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#17191c\" > <tbody> <tr> <td style=\" padding-top: 30px; padding-bottom: 30px; padding-left: 56px; padding-right: 56px; \" > <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" > <tbody> <tr> <td style=\" font-size: 18px; line-height: 25px; font-family: Arial, sans-serif, 'Motiva Sans'; color: #8f98a0; text-align: center; \" > Use this code to restore your password </td> </tr> <tr> <td style=\"padding-bottom: 16px\"></td> </tr> <tr> <td style=\" font-size: 48px; line-height: 52px; font-family: Arial, sans-serif, 'Motiva Sans'; color: #3a9aed; font-weight: bold; text-align: center; \" > {randomCode} </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            smtp.Send(message);
        }

        public string RandomCode(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}