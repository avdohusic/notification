namespace Notification.Model.Settings
{
    public class NotificationSettings
    {
        public int Repeat { get; set; }
        public int WaitSeconds { get; set; }
        public string SendgridApiKey { get; set; }
    }
}
