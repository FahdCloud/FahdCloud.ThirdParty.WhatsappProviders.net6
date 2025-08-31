using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Elwhats;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Elwhats.Responses;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Ultramsg.Response;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services
{
    public class ElwhatsService : IElwhatsService
    {
        private readonly IHttpClientService _httpClientService;
        private const string BaseUrl = "https://api2.elwhats.com";

        public ElwhatsService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<bool> CheckConnectionAsync(ElwhatsSetting settings)
        {
            try
            {
                var url = $"{BaseUrl}/phone/status";
                var queryParams = new Dictionary<string, string>
                {
                    ["clientId"] = settings.ClientId
                };

                var response = await _httpClientService.GetAsync<ElwhatsConnectionReponse>(url, queryParams);
                return response.isLinked ?? false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SendMessageResponse> SendMediaMessageAsync(ElwhatsSetting settings, string phoneNumber, string messageText, string mediaUrl)
        {
            var url = $"{BaseUrl}/send-messages";

            var data = new ElwhatsSendMediaMessageRequest
            {
                clientId = settings.ClientId,
                message = messageText,
                mediaUrl = mediaUrl,
                phone = phoneNumber,
                priority = 1,
                retryOnFailure = true
            };

            var (res, status) = await _httpClientService.PostWithStatusAsync<ElwhatsSendMessageResponse>(url, data);

            if (status != 200)
                return new SendMessageResponse 
                { 
                    isSuccess = false, 
                    meessage = $"Failed to send message via Elwhats API. Response: {res?.message ?? "No error message provided"}" 
                };

            return new SendMessageResponse { isSuccess = true, meessage = "success" };
        }

        public async Task<SendMessageResponse> SendMessageAsync(ElwhatsSetting settings, string phoneNumber, string messageText)
        {
            var url = $"{BaseUrl}/send-messages";

            var data = new ElwhatsSendMessageRequest
            {
                clientId = settings.ClientId,
                message = messageText,
                phone = phoneNumber,
                priority = 1,
                retryOnFailure = true
            };

            var (res, status) = await _httpClientService.PostWithStatusAsync<ElwhatsSendMessageResponse>(url, data);

            if (status != 200)
                return new SendMessageResponse 
                { 
                    isSuccess = false, 
                    meessage = $"Failed to send message via Elwhats API. Response: {res?.message ?? "No error message provided"}" 
                };

            return new SendMessageResponse { isSuccess = true, meessage = "success" };
        }
    }
}
