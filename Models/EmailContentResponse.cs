using System.Collections.Generic;

namespace Models
{
    public class EmailContentResponse
    {
        public List<NotificationQueue> Items { get; set; }
        public int Total { get; set; }
    }
}
