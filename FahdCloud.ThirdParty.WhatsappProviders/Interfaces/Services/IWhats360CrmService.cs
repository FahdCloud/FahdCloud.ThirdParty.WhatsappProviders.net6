using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Crm;

namespace FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for interacting with the Whats360 WhatsApp service provider.
    /// Provides methods to check connection status and send text or media messages.
    /// </summary>
    public interface IWhats360CrmService
    {
        /// <summary>
        /// Checks the connection status to the Whats360 service using the provided settings.
        /// </summary>
        /// <param name="whatsappCrmSetting">The configuration settings for the Whats360 provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning true if the connection is successful, false otherwise.</returns>
        Task<bool> CheckConnectionAsync(Whats360CrmSetting whatsappCrmSetting);

        /// <summary>
        /// Sends a text message to multiple phone numbers using the Whats360 provider.
        /// </summary>
        /// <param name="whatsappCrmSetting">The configuration settings for the Whats360 provider.</param>
        /// <param name="userPhones">A collection of phone numbers to send the message to.</param>
        /// <param name="messageText">The text content of the message to send.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<SendMessageResponse> SendMessageAsync(Whats360CrmSetting whatsappCrmSetting, string userPhone, string messageText);

        /// <summary>
        /// Sends a media message to a single phone number using the Whats360 provider.
        /// </summary>
        /// <param name="crmSettings">The configuration settings for the Whats360 provider.</param>
        /// <param name="phoneNumber">The phone number to send the media message to.</param>
        /// <param name="mediaUrl">The URL of the media to send.</param>
        /// <param name="mediaType">The type of media being sent (e.g., image, video, document).</param>
        /// <param name="caption">An optional caption for the media.</param>
        /// <param name="filename">An optional filename for the media, if applicable.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<SendMessageResponse> SendMediaMessageAsync(Whats360CrmSetting crmSettings, string phoneNumber, string mediaUrl, EnumWhats36oMediaType mediaType, string caption = "", string? filename = null);
    }
}