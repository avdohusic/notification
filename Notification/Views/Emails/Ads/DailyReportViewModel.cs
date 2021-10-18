using System.Collections.Generic;
using Notification.Common;

namespace Notification.Views.Emails.Ads
{
    public class DailyReportViewModel
    {
        public string FullName { get; set; }
        public string LogDate { get; set; }
        public int ActiveDomain { get; set; }
        public int InactiveDomain { get; set; }
        public int AdsNotEqual { get; set; }
        public int AdsNotFound { get; set; }
        public IList<AdsItemDetails> AdsItemError { get; set; }
    }
}
