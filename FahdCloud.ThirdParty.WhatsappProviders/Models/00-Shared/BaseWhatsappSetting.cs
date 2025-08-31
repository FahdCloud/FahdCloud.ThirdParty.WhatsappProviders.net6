namespace FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared
{
    /// <summary>
    /// Represents the base configuration settings for WhatsApp service providers.
    /// Serves as a foundation for specific provider settings.
    /// </summary>
    public class BaseWhatsappSetting
    {
        /// <summary>
        /// Gets or sets the user data information for the WhatsApp provider.
        /// This property is optional and may contain additional user-specific configuration.
        /// </summary>
        public WhatsappInfoUserData? WhatsappInfoUserData { get; set; }
    }
}