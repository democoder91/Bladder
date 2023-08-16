namespace Bladder.Services
{
    public interface IEmailService
    {
        public void Send(string emailFrom, string emailTo, string subject, string body);
    }
}
