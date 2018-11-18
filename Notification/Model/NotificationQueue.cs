using System;
using System.ComponentModel.DataAnnotations;

namespace Notification.Model
{
    public class NotificationQueue
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string From { get; set; }

        [Required]
        [StringLength(150)]
        public string To { get; set; }

        [StringLength(300)]
        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime CreatedTimestamp { get; set; }

        public DateTime? FirstAttemptedTimestamp { get; set; }

        public DateTime? LastAttemptedTimestamp { get; set; }

        public DateTime? SentTimestamp { get; set; }

        public DateTime? ExpiredTimestamp { get; set; }

        public short FailCount { get; set; }
    }
}
