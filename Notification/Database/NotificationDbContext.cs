using Microsoft.EntityFrameworkCore;
using Notification.Database.Seed;
using Notification.Model;

namespace Notification.Database
{
    public class NotificationDbContext: DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
        : base(options)
        { }

        public DbSet<NotificationQueue> NotificationQueues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<NotificationQueue>().HasData(NotificationQueueSeed.Get());
        }
    }
}
