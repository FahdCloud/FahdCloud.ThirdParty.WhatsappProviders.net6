using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Ultramsg;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Ultramsg.Response;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services
{
    public class UltramsgService : IUltramsgService
    {
        private readonly IHttpClientService _httpClientService;
        private const string BaseUrl = "https://api.ultramsg.com";

        public UltramsgService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<bool> CheckConnectionAsync(UltramsgSetting settings)
        {
            try
            {
                var url = $"{BaseUrl}/{settings.InstanceId}/instance/me";

                var queryParams = new Dictionary<string, string>
                {
                    ["token"] = settings.Token
                };

                var response = await _httpClientService.GetAsync<UltramsgSessionDetails>(url, queryParams);
                return !string.IsNullOrEmpty(response?.id);
            }
            catch
            {
                return false;
            }
        }

        public async Task<UltramsgSessionDetails> GetSessionDetailsAsync(UltramsgSetting settings)
        {
            try
            {
                var url = $"{BaseUrl}/{settings.InstanceId}/instance/me";

                var queryParams = new Dictionary<string, string>
                {
                    ["token"] = settings.Token
                };

                return await _httpClientService.GetAsync<UltramsgSessionDetails>(url, queryParams);
            }
            catch
            {
                return new UltramsgSessionDetails();
            }
        }

        public async Task<SendMessageResponse> SendMessageAsync(UltramsgSetting settings, string phoneNumber, string messageText)
        {
            var url = $"{BaseUrl}/{settings.InstanceId}/messages/chat";
            var data = new Dictionary<string, string>
            {
                ["token"] = settings.Token,
                ["to"] = phoneNumber,
                ["body"] = messageText
            };

            var res = await _httpClientService.PostAsync<UltraMsgSendMessageResponse>(url, data);

            if (res.message != "ok" && res.sent != "true")
            {
                return new SendMessageResponse
                {
                    isSuccess = false,
                    meessage = $"Failed to send message via UltraMsg API. Response: {res?.error ?? "No error message provided"}"
                };
            }

            return new SendMessageResponse
            {
                isSuccess = res is { message: "ok", sent: "true" },
                meessage = "success"
            };
        }

        public async Task<SendMessageResponse> SendMediaMessageAsync(
            UltramsgSetting settings,
            string phoneNumber,
            string mediaUrl,
            EnumMediaType mediaType,
            string caption = "",
            string? filename = null)
        {
            var url = $"{BaseUrl}/{settings.InstanceId}/messages/{mediaType}";
            var data = new Dictionary<string, string>
            {
                ["token"] = settings.Token,
                ["to"] = phoneNumber,
                [mediaType.ToString()] = mediaUrl,
                ["caption"] = caption ?? string.Empty
            };

            if (!string.IsNullOrEmpty(filename))
                data["filename"] = filename;

            var res = await _httpClientService.PostAsync<UltraMsgSendMessageResponse>(url, data);

            if (res.message != "ok" && res.sent != "true")
            {
                return new SendMessageResponse
                {
                    isSuccess = false,
                    meessage = $"Failed to send message via UltraMsg API. Response: {res?.error ?? "No error message provided"}"
                };
            }

            return new SendMessageResponse
            {
                isSuccess = res is { message: "ok", sent: "true" },
                meessage = "success"
            };
        }
    }
}
