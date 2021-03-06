﻿using System.Net.Mail;
using Swallow.BusinessLogic.Interfaces;

namespace Swallow.BusinessLogic
{
    public sealed class MailProcessor : IMailProcessor
    {
        public void Process(Mail mailData)
        {
            var email = new MailMessage
                {
                    From = new MailAddress(mailData.Sender),
                    Subject = mailData.Subject,
                    Body = mailData.Body
                };
            mailData.Receivers.ForEach(x => email.To.Add(new MailAddress(x)));

            var client = new SmtpClient
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.google.com"
                };

            client.Send(email);
        }
    }
}