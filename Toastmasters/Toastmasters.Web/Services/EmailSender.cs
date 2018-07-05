using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Toastmasters.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailCredentials _credentials;

        public EmailSender(IOptions<EmailCredentials> credentials)
        {
            _credentials = credentials.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("agwoow.ga", "dougmany@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(_credentials.Server, 587, SecureSocketOptions.StartTls);
                client.Authenticate(_credentials.Username, _credentials.Password);
                await client.SendAsync(emailMessage);
                client.Disconnect(true);

            }

        }

        public async Task SendNotifyEmail(String subject, String message)
        {
             await SendEmailAsync(_credentials.NotifyEmail, subject, message);
        }
    }

    public class EmailCredentials
    {
        public String Server { get; set; }
        public String Username{get;set; }
        public String Password { get; set; }
        public String NotifyEmail { get; set; }
    }
}
