using System.Collections.Generic;
using Notification.Common;

namespace Notification.ModelDao
{
    public class SendEmails
    {
        public List<EmailRecipients> EmailRecipients { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string EmailContent { get; set; }
        public string Attachment { get; set; }
    }
}
