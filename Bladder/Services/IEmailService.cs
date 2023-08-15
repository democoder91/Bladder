namespace Bladder.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string toAddress, string subject, string body);
    }
}
