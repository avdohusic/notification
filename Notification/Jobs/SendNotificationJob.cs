using System.Threading.Tasks;
using Notification.Interfaces;
using Quartz;

namespace Notification.Jobs
{
    public class SendNotificationJob : ISendNotificationJob
    {
        private readonly IEmailService _emailService;

        public SendNotificationJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var notificationQueue = _emailService.GetNotificationQueue();

            if (notificationQueue != null)
                await _emailService.ProceedNotificationAsync(notificationQueue);
        }
    }
}