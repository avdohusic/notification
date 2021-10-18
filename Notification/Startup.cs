using System.Linq;
using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Models.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Notification.Interfaces;
using Notification.Jobs;
using Notification.Services;
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
            var connectionString = Configuration.GetValue<string>("ConnectionStrings:ConnectionString");
            services.AddDbContext<NotificationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Notification services", Version = "v1"});
                c.ResolveConflictingActions(apiDescription => apiDescription.First());
            });

            services.Configure<NotificationSettings>(Configuration.GetSection("NotificationSettings"));
            services.Configure<TemplateSettings>(Configuration.GetSection("TemplateSettings"));
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IJobFactory, JobFactory>();
            services.AddTransient<ISendNotificationJob, SendNotificationJob>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailTemplateServices, EmailTemplateServices>();

            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NotificationDbContext notificationDbContext)
        {
            notificationDbContext.Database.EnsureCreated();
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app
                .UseRouting()
                .UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification service - V1"); })
                //.UseHttpsRedirection()
                .UseEndpoints(endpoints => {
                    endpoints.MapControllers();
                    endpoints.MapRazorPages();
                    endpoints.MapGet("/",
                        async context => { await context.Response.WriteAsync("Notification Service!"); });
                });
        }
    }
}