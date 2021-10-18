using System.Threading.Tasks;
using Models;

namespace Notification.Interfaces
{
    public interface IEmailService
    {
        NotificationQueue GetNotificationQueue();
        Task ProceedNotificationAsync(NotificationQueue notificationQueue);
    }
}