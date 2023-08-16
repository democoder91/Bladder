using Hangfire;

namespace Bladder.Services
{
    public class HangfireJobs
    {
        private readonly IBladderExpirationService _bladderExpirationService;

        public HangfireJobs(IBladderExpirationService bladderExpirationService)
        {
            _bladderExpirationService = bladderExpirationService;
        }

        public void ScheduleBladderExpirationCheck()
        {
            RecurringJob.AddOrUpdate("CheckBladderExpiration", () => _bladderExpirationService.CheckAndSendNotificationsAsync(), Cron.Hourly);
        }
    }

}
