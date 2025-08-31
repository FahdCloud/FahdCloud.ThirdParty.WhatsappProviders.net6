using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Elwhats;

namespace FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services
{
    /// <summary>
    /// Provides methods for interacting with the Elwhats WhatsApp service provider.
    /// This interface defines functionalities such as connection health checks and sending messages.
    /// </summary>
    public interface IElwhatsService
    {
        /// <summary>
        /// Checks the connection status with the Elwhats WhatsApp service provider.
        /// </summary>
        /// <param name="settings">The configuration settings required to connect to the Elwhats service provider.</param>
        /// <returns>A boolean value indicating whether the connection to the Elwhats service provider is active.</returns>
        Task<bool> CheckConnectionAsync(ElwhatsSetting settings);

        /// <summary>
        /// Sends a media message to the specified phone number using the Elwhats service provider.
        /// </summary>
        /// <param name="settings">The Elwhats configuration settings required for authentication.</param>
        /// <param name="phoneNumber">The recipient's phone number in international format.</param>
        /// <param name="messageText">The text message to be sent along with the media file.</param>
        /// <param name="mediaUrl">The URL of the media file to be sent (e.g., image, video).</param>
        /// <returns>An instance of <see cref="SendMessageResponse"/> containing the result of the send operation.</returns>
        Task<SendMessageResponse> SendMediaMessageAsync(ElwhatsSetting settings, string phoneNumber, string messageText, string mediaUrl);

        /// <summary>
        /// Sends a message via the Elwhats WhatsApp service provider.
        /// </summary>
        /// <param name="settings">The configuration settings for the Elwhats service, including client credentials.</param>
        /// <param name="phoneNumber">The phone number of the recipient in the proper format.</param>
        /// <param name="messageText">The text of the message to be sent.</param>
        /// <returns>A <see cref="SendMessageResponse"/> object indicating whether the message was sent successfully, along with a status message.</returns>
        Task<SendMessageResponse> SendMessageAsync(ElwhatsSetting settings,string phoneNumber, string messageText);
    }
}