using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.WABotMaster;
using FahdCloud.ThirdParty.WhatsappProviders.Models.WABotMaster.Response;
using FahdCloud.ThirdParty.WhatsappProviders.Utils;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services
{
    public class WABotMasterService : IWABotMasterService
    {
        private readonly IHttpClientService _httpClientService;
        private const string BaseUrl = "https://wabotmaster-cloud.com/api";

        private static readonly Dictionary<string, string> JsonHeaders = new()
        {
            ["Content-Type"] = "application/json"
        };

        public WABotMasterService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<bool> CheckConnectionAsync(WABotMasterSetting settings)
        {
            try
            {
                var url = $"{BaseUrl}/reconnect";
                var queryParams = new Dictionary<string, string>
                {
                    ["access_token"] = settings.AccessToken,
                    ["instance_id"] = settings.InstanceId
                };

                var response = await _httpClientService.GetAsync<WABotMasterReconnectResponse>(url, queryParams);
                return string.Equals(response?.status, "success", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public async Task<WhatsappInfoUserData> GetSessionDetailsAsync(WABotMasterSetting settings)
        {
            try
            {
                var url = $"{BaseUrl}/reconnect";

                var queryParams = new Dictionary<string, string>
                {
                    ["access_token"] = settings.AccessToken,
                    ["instance_id"] = settings.InstanceId
                };

                var response = await _httpClientService.GetAsync<WABotMasterReconnectResponse>(url, queryParams);
                return response?.data ?? new WhatsappInfoUserData();
            }
            catch
            {
                return new WhatsappInfoUserData();
            }
        }

        public async Task<SendMessageResponse> SendMessageAsync(WABotMasterSetting settings, string phoneNumber, string messageText)
        {
            var url = $"{BaseUrl}/send";
            var request = new
            {
                number = UtilsClass.NormalizePhoneNumber(phoneNumber),
                type = "text",
                message = messageText,
                media_url = string.Empty,
                filename = string.Empty,
                instance_id = settings.InstanceId,
                access_token = settings.AccessToken
            };

            var res = await _httpClientService.PostAsync<WABotMasterSendMessageResponse>(url, request, JsonHeaders);

            if (res.status != "success")
            {
                return new SendMessageResponse
                {
                    isSuccess = false,
                    meessage = $"Failed to send message via WABotMaster API. {res.message}"
                };
            }

            return new SendMessageResponse { isSuccess = true, meessage = "success" };
        }

        public async Task<SendMessageResponse> SendMediaMessageAsync(WABotMasterSetting settings, string phoneNumber, string mediaUrl, EnumMediaType mediaType, string caption = "", string? filename = null)
        {
            var url = $"{BaseUrl}/send";
            var request = new
            {
                number = UtilsClass.NormalizePhoneNumber(phoneNumber),
                type = mediaType.ToString(),
                message = caption ?? string.Empty,
                media_url = mediaUrl,
                filename = filename ?? string.Empty,
                instance_id = settings.InstanceId,
                access_token = settings.AccessToken
            };

            var res = await _httpClientService.PostAsync<WABotMasterSendMessageResponse>(url, request, JsonHeaders);

            if (res.status != "success")
            {
                return new SendMessageResponse
                {
                    isSuccess = false,
                    meessage = $"Failed to send message via WABotMaster API. {res.message}"
                };
            }

            return new SendMessageResponse { isSuccess = true, meessage = "success" };
        }
    }
}
