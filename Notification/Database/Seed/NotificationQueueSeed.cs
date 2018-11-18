using Notification.Model;
using System;

namespace Notification.Database.Seed
{
    public class NotificationQueueSeed
    {
        public static object[] Get()
        {
            return new object[]
            {
                new NotificationQueue
                {
                    Id = 1,
                    From = "notification.test@com",
                    To = "avdohusic@gmail.com",
                    Subject = "Test email notification",
                    Body = "This is content of the <b>Email</b> message",
                    CreatedTimestamp = DateTime.Now,
                    FailCount = 0
                },
                new NotificationQueue
                {
                    Id = 2,
                    From = "notification.test@com",
                    To = "avdo.husic@gmail.com",
                    Subject = "Test email notification",
                    Body = "This is content of the <b>Email</b> message",
                    CreatedTimestamp = DateTime.Now,
                    FailCount = 0
                }
            };
        }
    }
}
