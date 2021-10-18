using System;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using Models.Enum;
using Models.Settings;
using Notification.Common;
using Notification.Helper;
using Notification.Interfaces;
using Notification.ModelDao;
using Notification.Views.Emails.Ads;
using Notification.Views.Emails.General;

namespace Notification.Services
{
    public class EmailTemplateServices : IEmailTemplateServices
    {
        private readonly ILogger<EmailTemplateServices> _logger;
        private readonly NotificationDbContext _notificationDbContext;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        private readonly IOptions<TemplateSettings> _settings;
        private IWebHostEnvironment _hostingEnvironment;

        public EmailTemplateServices(NotificationDbContext notificationDbContext,
            ILogger<EmailTemplateServices> logger,
            IRazorViewToStringRenderer razorViewToStringRenderer,
            IWebHostEnvironment environment,
            IOptions<TemplateSettings> settings)
        {
            _notificationDbContext = notificationDbContext;
            _logger = logger;
            _razorViewToStringRenderer = razorViewToStringRenderer;
            _settings = settings;
            _hostingEnvironment = environment;
        }

        public async Task AddNotification(string body, EmailConf emailGeneral, string subject, string attachment = null)
        {
            try
            {
                var notification = new NotificationQueue
                {
                    Subject = subject,
                    Body = body,
                    Attachment = attachment,
                    To = emailGeneral.Email,
                    ToName = emailGeneral.FullName,
                    From = _settings.Value.From,
                    FromName = _settings.Value.FromName,
                    ReplyTo = _settings.Value.ReplyTo,
                    ReplyToName = _settings.Value.ReplyToName,
                    Priority = emailGeneral.Priority,
                    CreatedTimestamp = DateTime.Now,
                    FailCount = 0
                };
                _notificationDbContext.NotificationQueues.Add(notification);
                await _notificationDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public async Task PrepareDailyReport(DailyReportEmailDto dailyReportEmailDto)
        {
            if (dailyReportEmailDto.AdminEmails != null)
            {
                foreach (var item in dailyReportEmailDto.AdminEmails)
                {
                    var modelItem = new DailyReportViewModel
                    {
                        FullName = item.FullName,
                        InactiveDomain = dailyReportEmailDto.InactiveDomain,
                        ActiveDomain = dailyReportEmailDto.ActiveDomain,
                        AdsNotEqual = dailyReportEmailDto.AdsNotEqual,
                        AdsNotFound = dailyReportEmailDto.AdsNotFound,
                        AdsItemError = dailyReportEmailDto.AdsItemError,
                        LogDate = dailyReportEmailDto.LogDate
                    };
                    var body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/Ads/DailyReport.cshtml", modelItem);

                    await AddNotification(body, new EmailConf(item, dailyReportEmailDto.Priority), _settings.Value.DailyReport);
                }
            }
        }

        public async Task PrepareFileChanged(FileChangedEmailDao fileChanged)
        {
            if(fileChanged.AdminEmails != null) {
                foreach (var fileChangedAdminEmail in fileChanged.AdminEmails)
                {
                    var tmpFileChanged = new FileChangedViewModel
                    {
                        FullName = fileChangedAdminEmail.FullName,
                        AdsCheckerUrl = fileChanged.AdsCheckerUrl,
                        SellerDomain = fileChanged.SellerDomain,
                        FileChange = fileChanged.FileChange
                    };
                    var body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/Ads/FileChanged.cshtml", tmpFileChanged);

                    await AddNotification(body, new EmailConf(fileChangedAdminEmail, fileChanged.Priority), _settings.Value.FileChanged);
                }
            }
        }

        public async Task PrepareMissingFile(MissingFileEmailDao missingFile)
        {
            if (missingFile.AdminEmails != null)
            {
                foreach (var fileAdminEmail in missingFile.AdminEmails)
                {
                    var tmpViewModel = new MissingFileViewModel
                    {
                        FullName = fileAdminEmail.FullName,
                        AdsCheckerUrl = missingFile.AdsCheckerUrl,
                        SellerDomain = missingFile.SellerDomain
                    };
                    var body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/Ads/MissingFile.cshtml", tmpViewModel);

                    await AddNotification(body, new EmailConf(fileAdminEmail, missingFile.Priority), _settings.Value.FileChanged);
                }
            }
        }

        public async Task SellerUpdateAds(SellerUpdateAds sellerUpdateAds)
        {
            if (sellerUpdateAds.EmailRecipients != null)
            {
                var viewModel = new SellerUpdateAdsViewModel
                {
                    EmailContent = sellerUpdateAds.EmailContent
                };

                var attachmentFile = string.Empty;
                if (sellerUpdateAds.AdsContent != null)
                    attachmentFile = ImageUrlHelper.PrepareAdsFile(sellerUpdateAds.AdsContent);

                var body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/Ads/SellerUpdateAds.cshtml", viewModel);
                await AddNotification(body, new EmailConf(sellerUpdateAds.EmailRecipients, Priority.High), sellerUpdateAds.Subject, attachmentFile);
            }
        }

        public async Task SendEmails(SendEmails sendEmails)
        {
            if (sendEmails.EmailRecipients != null)
            {
                var viewModel = new SimpleEmailViewModel
                {
                    EmailContent = sendEmails.EmailContent,
                    EmailTitle = sendEmails.Title
                };
                foreach (var emailRecipient in sendEmails.EmailRecipients)
                {
                    var body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/General/SimpleEmail.cshtml", viewModel);
                    await AddNotification(body, new EmailConf(emailRecipient), sendEmails.Subject, sendEmails.Attachment);
                }
            }
        }

        public PagedList<NotificationQueue> GetAllEmails(ResourceParameters resourceParameters)
        {
            var query = _notificationDbContext.NotificationQueues.AsQueryable();
            if (!string.IsNullOrEmpty(resourceParameters.SearchString))
            {
                query = query.Where(x => x.To.StartsWith(resourceParameters.SearchString) ||
                                         x.Subject.StartsWith(resourceParameters.SearchString) ||
                                         x.ToName.StartsWith(resourceParameters.SearchString));
            }

            query = query.OrderByDescending(x => x.Id);

            return PagedList<NotificationQueue>.ToPagedList(query, resourceParameters.PageNumber,
                resourceParameters.PageSize);
        }
    }
}