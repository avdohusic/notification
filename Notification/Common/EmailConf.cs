using Models.Enum;

namespace Notification.Common
{
    public class EmailConf : EmailRecipients
    {

        public EmailConf(EmailRecipients emailRecipient, Priority priority = Priority.Normal)
        {
            Email = emailRecipient.Email;
            FullName = emailRecipient.FullName;
            Priority = priority;
        }

        public Priority Priority { get; internal set; } = Priority.Normal;
    }
}
