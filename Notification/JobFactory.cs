using Quartz;
using Quartz.Spi;
using System;

namespace Notification
{
    public class JobFactory : IJobFactory
    {
        //TypeFactory is just the DI Container of your choice
        private readonly IServiceProvider _container;

        public JobFactory(IServiceProvider container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return _container.GetService(bundle.JobDetail.JobType) as IJob;
            }
            catch (Exception e)
            {
                //Log the error and return null
                //every exception thrown will be swallowed by Quartz 
                return null;
            }
        }

        public void ReturnJob(IJob job)
        {
            // TODO
            //Don't forget to implement this,
            //or the memory will not released
            //Factory.Release(job);
        }
    }
}
