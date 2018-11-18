using Notification.Model;
using System.Threading.Tasks;

namespace Notification.Email
{
    public interface IEmailService
    {
        Task<NotificationQueue> SendEmail(NotificationQueue notificationQueue);
        void UpdateNotificationQueue(NotificationQueue notificationQueue);
        NotificationQueue GetNotificationQueue();
        Task ProceedNotificationAsync(NotificationQueue notificationQueue);
    }
}
