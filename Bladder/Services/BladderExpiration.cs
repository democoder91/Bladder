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

    public interface IBladderExpirationService
    {
        Task CheckAndSendNotificationsAsync();
    }

    public class BladderExpirationService : IBladderExpirationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly BladderDbContext context;

        public BladderExpirationService(IServiceProvider serviceProvider, BladderDbContext context)
        {
            _serviceProvider = serviceProvider;
            this.context = context;
        }

        public async Task CheckAndSendNotificationsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var bladderService = scope.ServiceProvider.GetRequiredService<IBuildingBladderService>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>(); // Implement your email service or use a library like SendGrid
            
            var expiringBladders = await context.Bladders.Where(b => b.ExpiryDate < DateTime.Now.AddHours(1)).Where(b => !b.ExpiryNotificationSent).ToListAsync();  // Modify the condition as needed
            if      (!expiringBladders.Any()) 
            {
                Console.WriteLine("no expiring bladders");
                Console.WriteLine(DateTime.Now);

            }
            foreach (var bladder in expiringBladders)
            {
                // Send email notification to users
                var emails = new List<string>() {"demo.composer2@gmail.com", "demo.composer4@gmail.com" };
                foreach (var userEmail in emails)
                {
                    emailService.Send("noReply@Bladder.com", userEmail, "Bladder Expiration Reminder", $"Your bladder with code {bladder.BladderCode} is about to expire.");
                }
                bladder.ExpiryNotificationSent = true;
                context.Bladders.Update(bladder);
                await context.SaveChangesAsync();
            }
        }
    }

}
