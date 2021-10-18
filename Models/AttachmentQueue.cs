using System.Text.Json.Serialization;

namespace Models
{
    public class AttachmentQueue
    {
        [JsonPropertyName("fileName")] public string FileName { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("disposition")] public string Disposition { get; set; }
        [JsonPropertyName("contentId")] public string ContentId { get; set; }
    }
}
