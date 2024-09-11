using System.Net;
using System.Net.Mail;

namespace StudentRecordManagement.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _username;
        private readonly string _password;

        public EmailSender(IConfiguration configuration)
        {
            _username = configuration["Mail:Username"];
            _password = configuration["Mail:Password"];
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_username, _password)
            };

            await client.SendMailAsync(
                new MailMessage(from: _username, to: email, subject, message));
        }
    }
}
