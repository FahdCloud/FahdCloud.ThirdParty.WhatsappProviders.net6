using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;

namespace FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Pro
{
    /// <summary>
    /// Represents configuration settings specific to the Whats360 Pro WhatsApp service provider.
    /// Inherits from <see cref="BaseWhatsappSetting"/> to include common WhatsApp settings.
    /// </summary>
    public class Whats360ProSetting : BaseWhatsappSetting
    {
        /// <summary>
        /// Gets or sets the API token for the Whats360 Pro provider.
        /// Required for authenticating API requests.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the instance ID for the Whats360 Pro provider.
        /// Identifies the specific instance of the service.
        /// </summary>
        public string InstanceId { get; set; } = string.Empty;
    }
}
