using System.Collections.Generic;
using Notification.Common;

namespace Notification.ModelDao
{
    public class FileChangedEmailDao : EmailGeneral
    {
        public string SellerDomain { get; set; }
        public string AdsCheckerUrl { get; set; }
        public List<FileChangeLineDetail> FileChange { get; set; }
    }
}
