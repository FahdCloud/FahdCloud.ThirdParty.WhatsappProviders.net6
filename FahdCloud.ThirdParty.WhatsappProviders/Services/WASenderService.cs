using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.WASender;
using FahdCloud.ThirdParty.WhatsappProviders.Utils;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services
{
    public class WASenderService : IWASenderService
    {
        private readonly IHttpClientService _httpClientService;

        private const string BaseUrl = "https://wasenderapi.com/api";

        private static readonly Dictionary<string, string> JsonHeaders = new()
        {
            ["Content-Type"] = "application/json"
        };

        public WASenderService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<bool> CheckConnectionAsync(WASenderSetting settings)
        {
            try
            {
                var url = $"{BaseUrl}/status";
                var headers = CreateAuthHeaders(settings.ApiKey);

                var response = await _httpClientService.GetAsync<WASenderInstanceResponse>(url, null, headers);
                return response?.status == "connected";
            }
            catch
            {
                return false;
            }
        }

        public async Task<WhatsAppAccount> GetWhatsappSessionAsync(WASenderSetting settings)
        {
            try
            {
                const string url = $"{BaseUrl}/whatsapp-sessions";
                var headers = CreateAuthHeaders(settings.ApiKey);

                var response = await _httpClientService.GetAsync<WASenderGetAllSessionResponse>(url, null, headers);
                return response.data?.FirstOrDefault() ?? new WhatsAppAccount();
            }
            catch
            {
                return new WhatsAppAccount();
            }
        }

        public async Task<SendMessageResponse> SendMessageAsync(WASenderSetting settings, string phoneNumber, string messageText)
        {
            const string url = $"{BaseUrl}/send-message";
            var headers = CreateAuthHeaders(settings.ApiKey);

            var requestBody = new
            {
                to = UtilsClass.NormalizePhoneNumber(phoneNumber),
                text = messageText
            };

            var response = await _httpClientService.PostAsync<WASenderSendMessageResponse>(url, requestBody, headers);

            if (!response.success)
            {
                return new SendMessageResponse
                {
                    isSuccess = false,
                    meessage = $"Failed to send message via WASender API. Response: {response?.message ?? "No error message provided"}"
                };
            }

            return new SendMessageResponse { isSuccess = true, meessage = "success" };
        }

        public async Task<SendMessageResponse> SendMediaMessageAsync(WASenderSetting settings, string phoneNumber, string mediaUrl, EnumWASenderMediaType mediaType, string caption = "", string? filename = null)
        {
            const string url = $"{BaseUrl}/send-message";
            var headers = CreateAuthHeaders(settings.ApiKey);

            var requestBody = new
            {
                to = UtilsClass.NormalizePhoneNumber(phoneNumber),
                text = caption,
                imageUrl = mediaType == EnumWASenderMediaType.image ? mediaUrl : null,
                videoUrl = mediaType == EnumWASenderMediaType.video ? mediaUrl : null,
                audioUrl = mediaType == EnumWASenderMediaType.audio ? mediaUrl : null,
                documentUrl = mediaType == EnumWASenderMediaType.document ? mediaUrl : null,
                fileName = filename
            };

            var response = await _httpClientService.PostAsync<WASenderSendMessageResponse>(url, requestBody, headers);

            if (!response.success)
            {
                return new SendMessageResponse
                {
                    isSuccess = false,
                    meessage = $"Failed to send message via WASender API. Response: {response?.message ?? "No error message provided"}"
                };
            }

            return new SendMessageResponse { isSuccess = true, meessage = "success" };
        }

        private static Dictionary<string, string> CreateAuthHeaders(string apiKey)
        {
            var headers = new Dictionary<string, string>(JsonHeaders)
            {
                ["Authorization"] = $"Bearer {apiKey}"
            };
            return headers;
        }
    }
}
