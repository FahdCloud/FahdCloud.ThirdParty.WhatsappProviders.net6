namespace FahdCloud.ThirdParty.WhatsappProviders.Models.WASender
{
    public class WASenderInstanceResponse
    {
        public string? status { get; set; }
    }

    public class WASenderSendMessageResponse
    {
        public bool success { get; set; }
        public string message { get; set; } = string.Empty;
    }
}