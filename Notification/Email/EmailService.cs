using Microsoft.Extensions.Options;
using Notification.Database;
using Notification.Model;
using Notification.Model.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Notification.Email
{
    public class EmailService : IEmailService
    {
        private readonly NotificationDbContext _notificationDbContext;
        private readonly IOptions<NotificationSettings> _settings;

        public EmailService(NotificationDbContext notificationDbContext, IOptions<NotificationSettings> settings)
        {
            _notificationDbContext = notificationDbContext;
            _settings = settings;
        }

        public async Task<NotificationQueue> SendEmail(NotificationQueue notificationQueue)
        {
            try
            {
                var sendGridClient = new SendGridClient(_settings.Value.SendgridApiKey);
                var sendGridMessage = CreateSendGridMessage(notificationQueue);

                var sendGridResponse = await sendGridClient.SendEmailAsync(sendGridMessage);

                if (sendGridResponse.StatusCode == HttpStatusCode.Accepted)
                    notificationQueue.SentTimestamp = DateTime.Now;
                else
                    notificationQueue.FailCount++;

                return notificationQueue;
            }
            catch (Exception e)
            {
                // TODO Logging
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task ProceedNotificationAsync(NotificationQueue notificationQueue)
        {
            try
            {
                if (notificationQueue.FirstAttemptedTimestamp == null)
                    notificationQueue.FirstAttemptedTimestamp = DateTime.Now;

                notificationQueue.LastAttemptedTimestamp = DateTime.Now;

                UpdateNotificationQueue(notificationQueue);

                var responseNotificationQueue = await SendEmail(notificationQueue);

                UpdateNotificationQueue(responseNotificationQueue);
            }
            catch (Exception e)
            {
                // TODO Logging
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateNotificationQueue(NotificationQueue notificationQueue)
        {
            try
            {
                _notificationDbContext.NotificationQueues.Update(notificationQueue);
                _notificationDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO Logging
                Console.WriteLine(e);
                throw;
            }
        }

        public NotificationQueue GetNotificationQueue()
        {
            try
            {
                var repeat = _settings.Value.Repeat;
                var waitSeconds = _settings.Value.WaitSeconds;
                var waitTime = DateTime.Now.AddSeconds(waitSeconds);

                var notificationQueue = _notificationDbContext.NotificationQueues.FirstOrDefault(x =>
                    (x.FirstAttemptedTimestamp == null || (x.LastAttemptedTimestamp < waitTime && x.SentTimestamp == null && x.ExpiredTimestamp == null)) &&
                    x.SentTimestamp == null &&
                    x.FailCount < repeat &&
                    (x.ExpiredTimestamp > DateTime.Now || x.ExpiredTimestamp == null));

                return notificationQueue;
            }
            catch (Exception e)
            {
                // TODO Logging
                Console.WriteLine(e);
                throw;
            }
        }

        private static SendGridMessage CreateSendGridMessage(NotificationQueue notificationQueue)
        {
            return MailHelper.CreateSingleEmail(
                new EmailAddress(notificationQueue.From),
                new EmailAddress(notificationQueue.To),
                notificationQueue.Subject, null, notificationQueue.Body);
        }
    }
}
