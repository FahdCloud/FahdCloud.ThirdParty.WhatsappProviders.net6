using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360;

namespace FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services
{
    public interface IWhats360V2Service
    {
        /// <summary>
        /// Checks the connection status to the Whats360 service using the provided settings.
        /// </summary>
        /// <param name="whats360V2Setting">The configuration settings for the Whats360 provider.</param>
        /// <returns>A task that represents the asynchronous operation, returning true if the connection is successful, false otherwise.</returns>
        Task<bool> CheckConnectionAsync(Whats360V2Setting whats360Setting);

        /// <summary>
        /// Sends a text message to multiple phone numbers using the Whats360 provider.
        /// </summary>
        /// <param name="whats360V2Setting">The configuration settings for the Whats360 provider.</param>
        /// <param name="userPhones">A collection of phone numbers to send the message to.</param>
        /// <param name="messageText">The text content of the message to send.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<SendMessageResponse> SendMessageAsync(Whats360V2Setting whats360Setting, string userPhone, string messageText);
    }
}