using System.Net;
using System.Net.Mail;
using Volo.Abp.Emailing;

namespace Bladder.Services
{
    public class EmailService:IEmailService 
    {

        

        public void Send(string emailFrom, string emailTo, string subject, string body)
        {
            MailAddress to = new MailAddress(emailTo);
            MailAddress from = new MailAddress(emailFrom);

            MailMessage email = new MailMessage(from, to);
            email.Subject = subject;
            email.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "sandbox.smtp.mailtrap.io";
            smtp.Port = 465;
            smtp.Credentials = new NetworkCredential("2f9ed9fd3dca3e", "cb0e75e6458033");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            try
            {
                /* Send method called below is what will send off our email 
                 * unless an exception is thrown.
                 */
                smtp.Send(email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



    }
}
