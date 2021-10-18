using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Notification.Helper;
using Notification.Interfaces;
using Notification.ModelDao;

namespace Notification.Controllers
{
    [Route("api/v1/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailTemplateServices _emailTemplateServices;

        public EmailController(IEmailTemplateServices emailTemplateServices)
        {
            _emailTemplateServices = emailTemplateServices;
        }

        [HttpPost("send-simple-emails")]
        public async Task SendEmails([FromBody] SendEmails sendEmails)
        {
            await _emailTemplateServices.SendEmails(sendEmails);
        }

        [HttpPost("seller-update-ads")]
        public async Task SendFileChanged([FromBody] SellerUpdateAds sellerUpdateAds)
        {
            await _emailTemplateServices.SellerUpdateAds(sellerUpdateAds);
        }

        [HttpPost("file-changed")]
        public async Task SendFileChanged([FromBody] FileChangedEmailDao fileChanged)
        {
            await _emailTemplateServices.PrepareFileChanged(fileChanged);
        }

        [HttpPost("missing-file")]
        public async Task SendMissingFile([FromBody] MissingFileEmailDao missingFile)
        {
            await _emailTemplateServices.PrepareMissingFile(missingFile);
        }

        [HttpPost("daily-report")]
        public async Task SendDailyReport([FromBody] DailyReportEmailDto dailyReportEmailDto)
        {
            await _emailTemplateServices.PrepareDailyReport(dailyReportEmailDto);
        }

        [HttpGet("get-all-emails")]
        public EmailContentResponse GetAllEmails([FromBody] ResourceParameters resourceParameters)
        {
            var items = _emailTemplateServices.GetAllEmails(resourceParameters);
            var res = new EmailContentResponse
            {
                Items = items,
                Total = items.TotalCount
            };

            return res;
        }
    }
}