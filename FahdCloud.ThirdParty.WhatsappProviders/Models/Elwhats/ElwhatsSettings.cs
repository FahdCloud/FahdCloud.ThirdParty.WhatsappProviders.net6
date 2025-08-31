using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;

namespace FahdCloud.ThirdParty.WhatsappProviders.Models.Elwhats
{
    /// <summary>
    /// Represents configuration settings specific to the Elwhats WhatsApp service provider.
    /// Inherits from <see cref="BaseWhatsappSetting"/> to include common WhatsApp settings.
    /// </summary>
    public class ElwhatsSetting : BaseWhatsappSetting
    {
        /// <summary>
        /// Gets or sets the authentication Client ID for the Elwhats provider.
        /// Required for authenticating API requests.
        /// </summary>
        public string ClientId { get; set; } = string.Empty;
    }
}