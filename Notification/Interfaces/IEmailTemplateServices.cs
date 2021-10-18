using System.Threading.Tasks;
using Models;
using Notification.Helper;
using Notification.ModelDao;

namespace Notification.Interfaces
{
    public interface IEmailTemplateServices
    {
        Task PrepareFileChanged(FileChangedEmailDao fileChanged);
        Task PrepareMissingFile(MissingFileEmailDao missingFile);
        Task PrepareDailyReport(DailyReportEmailDto dailyReportEmailDto);
        Task SellerUpdateAds(SellerUpdateAds sellerUpdateAds);
        Task SendEmails(SendEmails sendEmails);
        PagedList<NotificationQueue> GetAllEmails(ResourceParameters resourceParameters);
    }
}