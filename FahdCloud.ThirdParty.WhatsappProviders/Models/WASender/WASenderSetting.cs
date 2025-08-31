using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;

namespace FahdCloud.ThirdParty.WhatsappProviders.Models.WASender
{
    /// <summary>
    /// Represents configuration settings specific to the WASender WhatsApp service provider.
    /// Inherits from <see cref="BaseWhatsappSetting"/> to include common WhatsApp settings.
    /// </summary>
    public class WASenderSetting : BaseWhatsappSetting
    {
        /// <summary>
        /// Gets or sets the API key for the WASender provider.
        /// Required for authenticating API requests.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the instance ID for the WASender provider.
        /// Identifies the specific instance of the WASender service.
        /// </summary>
        public string InstanceId { get; set; } = string.Empty;
    }
}