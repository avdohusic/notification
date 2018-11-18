using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Database;
using Notification.Email;
using Notification.Jobs;
using Notification.Model.Settings;
using Notification.Service;
using Quartz.Spi;

namespace Notification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<NotificationDbContext>(options => options.UseSqlServer(connectionString));

            services.Configure<NotificationSettings>(Configuration.GetSection("Notification"));
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IJobFactory, JobFactory>();
            services.AddTransient<ISendNotificationJob, SendNotificationJob>();
            services.AddTransient<IEmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Notification Service!");
            });
        }
    }
}
