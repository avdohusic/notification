namespace Models.Settings
{
    public class NotificationSettings
    {
        public string Client { get; set; }
        public int Repeat { get; set; }
        public int WaitSeconds { get; set; }
        public string SendgridApiKey { get; set; }
        public SmtpSettings SmtpSettings { get; set; }
        public MailJetSettings MailJetSettings { get; set; }
    }

    public class MailJetSettings
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }

    public class SmtpSettings
    {
        public string SmtpServerAddress { get; set; }
        public int SmtpServerPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}