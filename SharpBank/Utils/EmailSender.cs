using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SharpBank.Utils
{
    public class EmailSender
    {
        private static string email = "sharpbankofficial@gmail.com";
        private static SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(email, "sharpBank1234"),
            EnableSsl = true
        };

        public static void SendEmail(string recipient, string subject, string body)
        {
            smtpClient.Send(email, recipient, subject, body);
        }

        public static void SendSignupConfirmationEmail(string recipient, string confirmationCode, string confirmationLink)
        {

            var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = "Thank you for registering with our services",
                Body = "<h1>Hello,</h1>" +
                "<h2>Thank you for registering with our services</h2> " +
                "<h3>Please enter this code: <b>" + confirmationCode + "<b></h3>" +
                "<h3>In this link <a href=\"" + confirmationLink + "\">Click here</a></h3>",
                IsBodyHtml = true
            };

            smtpClient.Send(mailMessage);
        }
    }
}
