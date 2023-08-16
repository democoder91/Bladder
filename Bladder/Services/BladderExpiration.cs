namespace Bladder.Services
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Bladder.Data;
    using Hangfire;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Volo.Abp.Identity;
    using Volo.Abp.PermissionManagement;

    public interface IBladderExpirationService
    {
        Task CheckAndSendNotificationsAsync();
    }

    public class BladderExpirationService : IBladderExpirationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly BladderDbContext context;
        private readonly IIdentityUserRepository userRepository;
        private readonly IPermissionManager permissionManager;

        public BladderExpirationService(
            IServiceProvider serviceProvider, 
            BladderDbContext context, 
            IIdentityUserRepository userRepository, 
            IPermissionManager permissionManager
            )
        {
            _serviceProvider = serviceProvider;
            this.context = context;
            this.userRepository = userRepository;
            this.permissionManager = permissionManager;
        }

        public async Task CheckAndSendNotificationsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var bladderService = scope.ServiceProvider.GetRequiredService<IBuildingBladderService>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>(); // Implement your email service or use a library like SendGrid
            
            var expiringBladders = await context.Bladders.Where(b => b.ExpiryDate < DateTime.Now.AddDays(7)).Where(b => !b.ExpiryNotificationSent).ToListAsync();  // Modify the condition as needed
            if      (!expiringBladders.Any()) 
            {
                Console.WriteLine("no expiring bladders");

            }
            foreach (var bladder in expiringBladders)
            {
                var emails = new List<string>() {};
                var users = await userRepository.GetListAsync();
                if (users != null)
                {
                    foreach (var user in users)
                    {
                        var userPermissions = await permissionManager.GetAllForUserAsync(user.Id);
                        foreach (var permission in userPermissions)
                        {
                            if (permission.Name == "NotifyForBladderExpiry" && permission.IsGranted)
                            {
                                emails.Add(user.Email);
                            }
                        }
                    }
                }
                // Send email notification to users
                foreach (var userEmail in emails)
                {
                    emailService.Send("noReply@Bladder.com", userEmail, "Bladder Expiration Reminder", $"<h1> A Bladder Is About To Expire </h1>\r\n\r\n<p>you are receiving this mail because the bladder with the code :  {bladder.BladderCode} , is abount to expire</p>\r\n<p>please take the necessary precautions</p>\r\n\r\n<div><span>please do not try to reply to this email, if you need any help please contact your technical support</span></div>\r\n<div><span>Wanna go to the Bladder Application?</span>   <a href=\"https://localhost:44374/\">click Here</a></div>");
                }
                if (emails.Count() > 0)
                {
                    bladder.ExpiryNotificationSent = true;
                    context.Bladders.Update(bladder);
                    await context.SaveChangesAsync();

                }
            }
        }
    }

}
