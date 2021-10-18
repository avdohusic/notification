using Microsoft.EntityFrameworkCore;
using Models;

namespace Database
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {
        }

        public DbSet<NotificationQueue> NotificationQueues { get; set; }
    }
}