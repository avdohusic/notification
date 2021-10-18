using System.Collections.Generic;
using Models.Enum;

namespace Notification.Common
{
    public class EmailGeneral
    {
        public Priority Priority { get; set; } = Priority.Normal;
        public List<EmailRecipients> AdminEmails { get; set; }
    }
}
