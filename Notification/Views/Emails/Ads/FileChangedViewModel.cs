using System.Collections.Generic;
using Notification.ModelDao;

namespace Notification.Views.Emails.Ads
{
    public class FileChangedViewModel
    {
        public string FullName { get; set; }
        public string SellerDomain { get; set; }
        public string AdsCheckerUrl { get; set; }

        public List<FileChangeLineDetail> FileChange { get; set; }
    }
}
