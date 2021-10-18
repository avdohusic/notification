using System.Collections.Specialized;
using Microsoft.Extensions.Logging;
using Notification.Interfaces;
using Notification.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IJobFactory _jobFactory;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IJobFactory jobFactory, ILogger<NotificationService> logger)
        {
            _jobFactory = jobFactory;
            _logger = logger;
        }

        public void Start()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                var props = new NameValueCollection
                {
                    {"quartz.serializer.type", "binary"}
                };
                var factory = new StdSchedulerFactory(props);
                var scheduler = factory.GetScheduler().Result;

                scheduler.JobFactory = _jobFactory;

                var job = JobBuilder.Create<ISendNotificationJob>()
                    .WithIdentity("SendNotificationJob", "Notification")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("SendNotificationTrigger", "Notification")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(5)
                        .RepeatForever())
                    .Build();

                scheduler.Start();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);

                // and last shut down the scheduler when you are ready to close your program
                // await scheduler.Shutdown
            }
            catch (SchedulerException se)
            {
                _logger.LogError(se.Message);
            }
        }
    }
}