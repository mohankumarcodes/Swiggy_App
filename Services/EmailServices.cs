using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace Swiggy_App.Services
{
    public class EmailServices
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailNotification(string subject, string messageBody)
        {
            //Access appSettings.json file elements
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var port=int.Parse(_configuration["EmailSettings:Port"]);
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var senderPassword = _configuration["EmailSettings:SenderPassword"];
            var receiverEmail = _configuration["EmailSettings:ReceiverEmail"];

            // create message in Email
            var emailMessage = new MimeMessage();// this MimeMessage() is from MimeKit Library
            emailMessage.From.Add(new MailboxAddress("Order System", senderEmail));
            emailMessage.To.Add(new MailboxAddress("Admin", receiverEmail));
            emailMessage.Subject = subject;
            emailMessage.Body=new TextPart("plain") { Text=messageBody};

            // Connecting  SmptServer 
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(senderEmail, senderPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

        }

    }
}
