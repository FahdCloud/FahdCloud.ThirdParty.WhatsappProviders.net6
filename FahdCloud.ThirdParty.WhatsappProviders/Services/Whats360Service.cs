using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Utils;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Crm;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services
{
    public class Whats360Service : IWhats360Service
    {
        private const string BaseUrl = "https://apis.whats360.live/api";
        private readonly IHttpClientService _httpClientService;

        public Whats360Service(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        public async Task<bool> CheckConnectionAsync(Whats360Setting crmSettings)
        {
            try
            {
                var url = $"{BaseUrl}/user/v2/check_wa";
                var queryParams = new Dictionary<string, string>
                {
                    ["mobile"] = crmSettings.PhoneNationalNumber,
                    ["token"] = crmSettings.ApiKey,
                    ["client_id"] = crmSettings.ClientId,
                };

                var response = await _httpClientService.GetAsync<Whats360Response>(url, queryParams);
                return response?.success == true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SendMessageResponse> SendMessageAsync(Whats360Setting crmSettings, string userPhone, string messageText)
        {

                var url = $"{BaseUrl}/user/v2/send_message_url";
                var headers = CreateAuthHeaders(crmSettings.ApiKey);
                var queryParams = new Dictionary<string, string>
                {
                    ["client_id"] = crmSettings.ClientId,
                    ["token"] = crmSettings.ApiKey,
                    ["mobile"] = userPhone,
                    ["text"] = messageText
                };

               var res =  await _httpClientService.GetAsync<Whats360Response>(url, queryParams, headers);

               if (!res.success)
                   return new SendMessageResponse
                   {
                       isSuccess = res.success,
                       meessage = $"Failed to send media message via Whats360 API. Response: {res?.message ?? "No error message provided"}"
                   };

               return new SendMessageResponse { isSuccess = res.success, meessage = "success"};
        }

        private static Dictionary<string, string> CreateAuthHeaders(string apiKey)
        {
            return new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {apiKey}",
                ["Content-Type"] = "application/json"
            };
        }
    }
}