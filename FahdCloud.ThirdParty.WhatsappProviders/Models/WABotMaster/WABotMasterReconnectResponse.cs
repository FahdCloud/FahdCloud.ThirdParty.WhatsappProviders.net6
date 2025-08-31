using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;

namespace FahdCloud.ThirdParty.WhatsappProviders.Models.WABotMaster
{
    public class WABotMasterReconnectResponse
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public WhatsappInfoUserData? data { get; set; }
    }
}