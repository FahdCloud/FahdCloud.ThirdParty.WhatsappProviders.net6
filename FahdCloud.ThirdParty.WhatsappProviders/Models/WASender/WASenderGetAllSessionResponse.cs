namespace FahdCloud.ThirdParty.WhatsappProviders.Models.WASender
{
    public class WASenderGetAllSessionResponse
    {
        public bool success { get; set; }
        public List<WhatsAppAccount>? data { get; set; }
    }
}