﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SharpBank.Utils
{
    public class EmailSender
    {
        private static string _email = "sharpbankofficial@gmail.com";
        private static SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(_email, "sharpBank1234"),
            EnableSsl = true
        };

        public static void SendEmail(string recipient, string subject, string body)
        {
            _smtpClient.Send(_email, recipient, subject, body);
        }

        public static async Task SendSignupConfirmationEmail(string recipient, string confirmationCode, string confirmationLink)
        {

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_email),
                Subject = "Thank you for registering with our services",
                Body = "<h2>Hello from SharpBank Team,</h2>" +
                "<p>Thank you for creating an account on our platform. To confirm your newly" +
                "created account, please follow the steps presented below</p> " +
                "<h3>Please enter this code: <b>" + confirmationCode + "<b></h3>" +
                "<h3>In this link <a href=\"" + confirmationLink + "\">Click here</a></h3>" +
                "<br><br><p>Kind regards,</p><p>SharpBank Team</p>",
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(recipient));

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
