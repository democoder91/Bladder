using System.Net.Mail;
using Volo.Abp.Emailing;

namespace Bladder.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendEmailAsync(string toAddress, string subject, string body)
        {


            await _emailSender.SendAsync(
                toAddress,     // target email address
                subject,         // subject
                body  // email body
            );
        }
    

        
    }
}
