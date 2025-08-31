using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Ultramsg;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Ultramsg.Response;

namespace FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for interacting with the Ultramsg WhatsApp service provider.
    /// Provides methods to check connection status and send text or media messages.
    /// </summary>
    public interface IUltramsgService
    {
        /// <summary>
        /// Checks the connection status to the Ultramsg service using the provided settings.
        /// </summary>
        /// <param name="whatsappSetting">The configuration settings for the Ultramsg provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning true if the connection is successful, false otherwise.</returns>
        Task<bool> CheckConnectionAsync(UltramsgSetting whatsappSetting);

        /// <summary>
        /// Sends a text message to multiple phone numbers using the Ultramsg provider.
        /// </summary>
        /// <param name="whatsappSetting">The configuration settings for the Ultramsg provider.</param>
        /// <param name="phoneNumber">A phone number to send the message to.</param>
        /// <param name="messageText">The text content of the message to send.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<SendMessageResponse> SendMessageAsync(UltramsgSetting whatsappSetting, string? phoneNumber, string messageText);

        /// <summary>
        /// Sends a media message to a single phone number using the Ultramsg provider.
        /// </summary>
        /// <param name="settings">The configuration settings for the Ultramsg provider.</param>
        /// <param name="phoneNumber">The phone number to send the media message to.</param>
        /// <param name="mediaUrl">The URL of the media to send.</param>
        /// <param name="mediaType">The type of media being sent (e.g., image, video, document).</param>
        /// <param name="caption">An optional caption for the media.</param>
        /// <param name="filename">An optional filename for the media, if applicable.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<SendMessageResponse> SendMediaMessageAsync(UltramsgSetting settings, string phoneNumber, string mediaUrl, EnumMediaType mediaType, string caption = "", string? filename = null);

        Task<UltramsgSessionDetails> GetSessionDetailsAsync(UltramsgSetting settings);
    }
}