namespace FahdCloud.ThirdParty.WhatsappProviders.Models.WASender
{
    public class WhatsAppAccount
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string status { get; set; }
        public bool accountProtection { get; set; }
        public bool logMessages { get; set; }
        public string webhookUrl { get; set; }
        public bool webhookEnabled { get; set; }
        public List<string> webhookEvents { get; set; }
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset updatedAt { get; set; }
    }
}