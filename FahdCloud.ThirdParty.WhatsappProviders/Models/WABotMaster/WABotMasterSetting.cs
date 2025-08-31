using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;

namespace FahdCloud.ThirdParty.WhatsappProviders.Models.WABotMaster
{
    /// <summary>
    /// Represents configuration settings specific to the WABotMaster WhatsApp service provider.
    /// Inherits from <see cref="BaseWhatsappSetting"/> to include common WhatsApp settings.
    /// </summary>
    public class WABotMasterSetting : BaseWhatsappSetting
    {
        /// <summary>
        /// Gets or sets the access token for the WABotMaster provider.
        /// Required for authenticating API requests.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the instance ID for the WABotMaster provider.
        /// Identifies the specific instance of the WABotMaster service.
        /// </summary>
        public string InstanceId { get; set; } = string.Empty;
    }
}