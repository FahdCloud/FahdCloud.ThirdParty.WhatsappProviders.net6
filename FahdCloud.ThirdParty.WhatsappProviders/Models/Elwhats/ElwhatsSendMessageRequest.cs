namespace FahdCloud.ThirdParty.WhatsappProviders.Models.Elwhats
{
    public class ElwhatsSendMessageRequest
    {
        public string? phone { get; set; }
        public string? message { get; set; }
        public string? clientId { get; set; }
        public bool? retryOnFailure { get; set; }
        public int? priority { get; set; } = 1;
    }

    public class ElwhatsSendMediaMessageRequest : ElwhatsSendMessageRequest
    {
        public string? mediaUrl { get; set; }
    }
}