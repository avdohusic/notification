using Models.Enum;
using Notification.Common;

namespace Notification.ModelDao
{
    public class SellerUpdateAds
    {
        public Priority Priority { get; set; } = Priority.Normal;
        public EmailRecipients EmailRecipients { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public string AdsContent { get; set; }
    }
}
