using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Pro;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Pro.Responses;

namespace FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for interacting with the Whats360 Pro WhatsApp service provider.
    /// Provides methods to manage instances and send text or media messages.
    /// </summary>
    public interface IWhats360ProService
    {
        /// <summary>
        /// Checks the connection status of the specified Whats360 Pro instance.
        /// </summary>
        /// <param name="settings">The configuration settings for the Whats360 Pro provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning true if the connection is successful, false otherwise.</returns>
        Task<bool> CheckConnectionAsync(Whats360ProSetting settings);

        /// <summary>
        /// Sends a text message using the Whats360 Pro provider.
        /// </summary>
        /// <param name="settings">The configuration settings for the Whats360 Pro provider.</param>
        /// <param name="jid">The recipient's JID (e.g., [phone]@s.whatsapp.net).</param>
        /// <param name="messageText">The text content of the message to send.</param>
        /// <returns>A task that represents the asynchronous operation, returning a SendMessageResponse.</returns>
        Task<SendMessageResponse> SendMessageAsync(Whats360ProSetting settings, string jid, string messageText);

        /// <summary>
        /// Sends a media message (image, video, audio, document) using the Whats360 Pro provider.
        /// </summary>
        /// <param name="settings">The configuration settings for the Whats360 Pro provider.</param>
        /// <param name="jid">The recipient's JID (e.g., [phone]@s.whatsapp.net).</param>
        /// <param name="mediaUrl">The URL of the media to send.</param>
        /// <param name="mediaType">The type of media being sent.</param>
        /// <param name="caption">An optional caption for the media (not supported for audio).</param>
        /// <returns>A task that represents the asynchronous operation, returning a SendMessageResponse.</returns>
        Task<SendMessageResponse> SendMediaMessageAsync(Whats360ProSetting settings, string jid, string mediaUrl, EnumWhats360ProMediaType mediaType, string caption = "");

        /// <summary>
        /// Retrieves all instances associated with the provided Whats360 Pro token.
        /// </summary>
        /// <param name="settings">The configuration settings for the Whats360 Pro provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning a Whats360ProBaseResponse.</returns>
        Task<Whats360ProBaseResponse> GetInstancesAsync(Whats360ProSetting settings);

        /// <summary>
        /// Creates a new instance in the Whats360 Pro service.
        /// </summary>
        /// <param name="settings">The configuration settings for the Whats360 Pro provider.</param>
        /// <param name="id">The unique identifier for the new instance.</param>
        /// <param name="name">An optional name for the new instance.</param>
        /// <returns>A task that represents the asynchronous operation, returning a Whats360ProBaseResponse.</returns>
        Task<Whats360ProBaseResponse> CreateInstanceAsync(Whats360ProSetting settings, string id, string? name = null);

        /// <summary>
        /// Connects or starts the specified Whats360 Pro instance.
        /// </summary>
        /// <param name="settings">The configuration settings for the Whats360 Pro provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning a Whats360ProBaseResponse.</returns>
        Task<Whats360ProBaseResponse> ConnectInstanceAsync(Whats360ProSetting settings);

        /// <summary>
        /// Disconnects or stops the specified Whats360 Pro instance.
        /// </summary>
        /// <param name="settings">The configuration settings for the Whats360 Pro provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning a Whats360ProBaseResponse.</returns>
        Task<Whats360ProBaseResponse> DisconnectInstanceAsync(Whats360ProSetting settings);

        /// <summary>
        /// Deletes the specified Whats360 Pro instance.
        /// </summary>
        /// <param name="settings">The configuration settings for the Whats360 Pro provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning a Whats360ProBaseResponse.</returns>
        Task<Whats360ProBaseResponse> DeleteInstanceAsync(Whats360ProSetting settings);
    }
}
