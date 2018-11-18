using Notification.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Collections.Specialized;

namespace Notification.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IJobFactory _jobFactory;

        public NotificationService(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        public void Start()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = factory.GetScheduler().Result;

                scheduler.JobFactory = _jobFactory;

                IJobDetail job = JobBuilder.Create<ISendNotificationJob>()
                    .WithIdentity("SendNotificationJob", "Notification")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
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
                // Logg error
            }
        }
    }
}
