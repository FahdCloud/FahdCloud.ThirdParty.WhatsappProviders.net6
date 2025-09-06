using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;

namespace FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360
{
    /// <summary>
    /// Represents configuration settings specific to the Whats360 WhatsApp service provider.
    /// Inherits from <see cref="BaseWhatsappSetting"/> to include common WhatsApp settings.
    /// </summary>
    public class Whats360V2Setting : BaseWhatsappSetting
    {
        /// <summary>
        /// Gets or sets the API key for the Whats360 provider.
        /// Required for authenticating API requests.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the client ID for the Whats360 provider.
        /// Identifies the specific client account in the Whats360 service.
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the national phone number associated with the Whats360 account.
        /// Used for sending messages or verifying the account.
        /// </summary>
        public string PhoneNationalNumber { get; set; } = string.Empty;
    }
}