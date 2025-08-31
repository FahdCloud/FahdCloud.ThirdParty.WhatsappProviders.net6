using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.WASender;

namespace FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for interacting with the WASender WhatsApp service provider.
    /// Provides methods to check connection status and send text or media messages.
    /// </summary>
    public interface IWASenderService
    {
        /// <summary>
        /// Checks the connection status to the WASender service using the provided settings.
        /// </summary>
        /// <param name="settings">The configuration settings for the WASender provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning true if the connection is successful, false otherwise.</returns>
        Task<bool> CheckConnectionAsync(WASenderSetting settings);

        /// <summary>
        /// Sends a text message to multiple phone numbers using the WASender provider.
        /// </summary>
        /// <param name="settings">The configuration settings for the WASender provider.</param>
        /// <param name="phoneNumber">A phone number to send the message to.</param>
        /// <param name="messageText">The text content of the message to send.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<SendMessageResponse> SendMessageAsync(WASenderSetting settings,string phoneNumber, string messageText);

        /// <summary>
        /// Sends a media message to a single phone number using the WASender provider.
        /// </summary>
        /// <param name="settings">The configuration settings for the WASender provider.</param>
        /// <param name="phoneNumber">The phone number to send the media message to.</param>
        /// <param name="mediaUrl">The URL of the media to send.</param>
        /// <param name="mediaType">The type of media being sent (e.g., image, video, document).</param>
        /// <param name="caption">An optional caption for the media.</param>
        /// <param name="filename">An optional filename for the media, if applicable.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<SendMessageResponse> SendMediaMessageAsync(WASenderSetting settings, string phoneNumber, string mediaUrl, EnumWASenderMediaType mediaType, string caption = "", string? filename = null);

        Task<WhatsAppAccount> GetWhatsappSessionAsync(WASenderSetting settings);
    }
}