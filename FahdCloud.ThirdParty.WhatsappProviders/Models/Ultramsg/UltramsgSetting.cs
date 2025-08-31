using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;

namespace FahdCloud.ThirdParty.WhatsappProviders.Models.Ultramsg
{
    /// <summary>
    /// Represents configuration settings specific to the Ultramsg WhatsApp service provider.
    /// Inherits from <see cref="BaseWhatsappSetting"/> to include common WhatsApp settings.
    /// </summary>
    public class UltramsgSetting : BaseWhatsappSetting
    {
        /// <summary>
        /// Gets or sets the authentication token for the Ultramsg provider.
        /// Required for authenticating API requests.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the instance ID for the Ultramsg provider.
        /// Identifies the specific instance of the Ultramsg service.
        /// </summary>
        public string InstanceId { get; set; } = string.Empty;
    }
}