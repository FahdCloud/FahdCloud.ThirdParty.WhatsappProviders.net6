using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Utils;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Crm;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services
{
    public class Whats360CrmService : IWhats360CrmService
    {
        private readonly IHttpClientService _httpClientService;
        private const string BaseUrl = "https://crm.whats360.live/api";

        public Whats360CrmService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<bool> CheckConnectionAsync(Whats360CrmSetting crmSettings)
        {
            try
            {
                var url = $"{BaseUrl}/v1/send-text";
                var headers = CreateAuthHeaders(crmSettings.ApiKey);
                var queryParams = new Dictionary<string, string>
                {
                    ["token"] = crmSettings.ApiKey,
                    ["instance_id"] = crmSettings.ClientId,
                    ["jid"] = $"{crmSettings.PhoneNationalNumber}@s.whatsapp.net",
                    ["msg"] = "Connection test"
                };

                var response = await _httpClientService.GetAsync<Whats360CrmResponse>(url, queryParams, headers);
                return response?.success == true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SendMessageResponse> SendMessageAsync(Whats360CrmSetting crmSettings, string userPhone, string messageText)
        {
            var url = $"{BaseUrl}/v1/send-text";
            var headers = CreateAuthHeaders(crmSettings.ApiKey);
            var queryParams = new Dictionary<string, string>
            {
                ["token"] = crmSettings.ApiKey,
                ["instance_id"] = crmSettings.ClientId,
                ["jid"] = FormatWhatsAppJid(userPhone),
                ["msg"] = messageText
            };

            var res = await _httpClientService.GetAsync<Whats360CrmResponse>(url, queryParams, headers);

            if (res?.success != true)
            {
                return new SendMessageResponse
                {
                    isSuccess = false,
                    meessage = $"Failed to send message via Whats360 API. Response: {res?.message ?? "No error message provided"}"
                };
            }

            return new SendMessageResponse { isSuccess = true, meessage = "success" };
        }

        public async Task<SendMessageResponse> SendMediaMessageAsync(Whats360CrmSetting crmSettings, string phoneNumber, string mediaUrl, EnumWhats36oMediaType mediaType, string caption = "", string? filename = null)
        {
            var url = $"{BaseUrl}/v1/send-{mediaType}";
            var headers = CreateAuthHeaders(crmSettings.ApiKey);
            var queryParams = CreateBaseQueryParams(crmSettings, phoneNumber);

            AddMediaParameters(queryParams, mediaType, mediaUrl, caption);

            var res = await _httpClientService.GetAsync<Whats360CrmResponse>(url, queryParams, headers);

            if (res?.success != true)
            {
                return new SendMessageResponse
                {
                    isSuccess = false,
                    meessage = $"Failed to send media message via Whats360 API. Response: {res?.message ?? "No error message provided"}"
                };
            }

            return new SendMessageResponse { isSuccess = true, meessage = "success" };
        }

        private static Dictionary<string, string> CreateAuthHeaders(string apiKey)
        {
            return new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {apiKey}",
                ["Content-Type"] = "application/json"
            };
        }

        private Dictionary<string, string> CreateBaseQueryParams(Whats360CrmSetting crmSettings, string phoneNumber)
        {
            return new Dictionary<string, string>
            {
                ["token"] = crmSettings.ApiKey,
                ["instance_id"] = crmSettings.ClientId,
                ["jid"] = FormatWhatsAppJid(phoneNumber)
            };
        }

        private static void AddMediaParameters(Dictionary<string, string> queryParams, EnumWhats36oMediaType mediaType, string mediaUrl, string? caption)
        {
            switch (mediaType)
            {
                case EnumWhats36oMediaType.image:
                    queryParams["imageurl"] = mediaUrl;
                    if (!string.IsNullOrEmpty(caption))
                        queryParams["caption"] = caption;
                    break;
                case EnumWhats36oMediaType.video:
                    queryParams["videourl"] = mediaUrl;
                    if (!string.IsNullOrEmpty(caption))
                        queryParams["caption"] = caption;
                    break;
                case EnumWhats36oMediaType.audio:
                    queryParams["audiourl"] = mediaUrl;
                    break;
                case EnumWhats36oMediaType.doc:
                    queryParams["docurl"] = mediaUrl;
                    if (!string.IsNullOrEmpty(caption))
                        queryParams["caption"] = caption;
                    break;
                default:
                    throw new ArgumentException($"Unsupported media type: {mediaType}");
            }
        }

        private static string FormatWhatsAppJid(string phoneNumber)
        {
            return $"{UtilsClass.NormalizePhoneNumber(phoneNumber)}@s.whatsapp.net";
        }
    }
}
