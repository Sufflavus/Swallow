using System.Net.Mail;

namespace Swallow.BusinessLogic
{
    public sealed class MailProcessor
    {
        public static void Process(Mail mailData)
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