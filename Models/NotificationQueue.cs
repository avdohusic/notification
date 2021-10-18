using System;
using System.ComponentModel.DataAnnotations;
using Models.Enum;

namespace Models
{
    public class NotificationQueue
    {
        public int Id { get; set; }
        [Required] [StringLength(200)] public string From { get; set; }
        [Required] [StringLength(200)] public string FromName { get; set; }
        [Required] [StringLength(1000)] public string To { get; set; }
        [StringLength(1000)] public string ToName { get; set; }
        [Required] [StringLength(200)] public string ReplyTo { get; set; }
        [StringLength(200)] public string ReplyToName { get; set; }
        [StringLength(300)] public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? FirstAttemptedTimestamp { get; set; }
        public DateTime? LastAttemptedTimestamp { get; set; }
        public DateTime? SentTimestamp { get; set; }
        public DateTime? ExpiredTimestamp { get; set; }
        public short FailCount { get; set; }
        public Priority Priority { get; set; }
    }
}