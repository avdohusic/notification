using System.Collections.Generic;
using Notification.Common;

namespace Notification.ModelDao
{
    public class DailyReportEmailDto : EmailGeneral
    {
        public string LogDate { get; set; }
        public int ActiveDomain { get; set; }
        public int InactiveDomain { get; set; }
        public int AdsNotEqual { get; set; }
        public int AdsNotFound { get; set; }
        public IList<AdsItemDetails> AdsItemError { get; set; }
    }
}
