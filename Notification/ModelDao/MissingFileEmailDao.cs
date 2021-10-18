using Notification.Common;

namespace Notification.ModelDao
{
    public class MissingFileEmailDao: EmailGeneral
    {
        public string SellerDomain { get; set; }
        public string AdsCheckerUrl { get; set; }
    }
}
