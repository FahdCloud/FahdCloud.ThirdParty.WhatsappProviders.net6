using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Pro;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Pro.Responses;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services
{
    /// <summary>
    /// Implements the <see cref="IWhats360ProService"/> for interacting with the Whats360 Pro WhatsApp service provider.
    /// </summary>
    public class Whats360ProService : IWhats360ProService
    {
        private const string BaseUrl = "https://pro.whats360.live/api/v1";
        private readonly IHttpClientService _httpClientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Whats360ProService"/> class.
        /// </summary>
        /// <param name="httpClientService">The HTTP client service used for making API requests.</param>
        public Whats360ProService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        /// <inheritdoc/>
        public async Task<bool> CheckConnectionAsync(Whats360ProSetting settings)
        {
            try
            {
                var queryParams = new Dictionary<string, string>
                {
                    ["token"] = settings.Token,
                    ["instance_id"] = settings.InstanceId
                };

                var response = await _httpClientService.GetAsync<Whats360ProBaseResponse>($"{BaseUrl}/instances/status", queryParams);
                return response?.success == true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<SendMessageResponse> SendMessageAsync(Whats360ProSetting settings, string jid, string messageText)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["token"] = settings.Token,
                ["instance_id"] = settings.InstanceId,
                ["jid"] = jid,
                ["msg"] = messageText
            };

            var response = await _httpClientService.GetAsync<Whats360ProBaseResponse>($"{BaseUrl}/send-text", queryParams);

            return new SendMessageResponse
            {
                isSuccess = response?.success ?? false,
                meessage = response?.success == true ? "success" : (response?.error ?? response?.message ?? "Unknown error")
            };
        }

        /// <inheritdoc/>
        public async Task<SendMessageResponse> SendMediaMessageAsync(Whats360ProSetting settings, string jid, string mediaUrl, EnumWhats360ProMediaType mediaType, string caption = "")
        {
            string endpoint = mediaType switch
            {
                EnumWhats360ProMediaType.image => "send-image",
                EnumWhats360ProMediaType.video => "send-video",
                EnumWhats360ProMediaType.audio => "send-audio",
                EnumWhats360ProMediaType.document => "send-doc",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaType), mediaType, null)
            };

            var queryParams = new Dictionary<string, string>
            {
                ["token"] = settings.Token,
                ["instance_id"] = settings.InstanceId,
                ["jid"] = jid,
                [mediaType == EnumWhats360ProMediaType.document ? "docurl" : 
                 mediaType == EnumWhats360ProMediaType.audio ? "audiourl" : 
                 mediaType == EnumWhats360ProMediaType.video ? "videourl" : "imageurl"] = mediaUrl
            };

            if (!string.IsNullOrEmpty(caption) && mediaType != EnumWhats360ProMediaType.audio)
            {
                queryParams["caption"] = caption;
            }

            var response = await _httpClientService.GetAsync<Whats360ProBaseResponse>($"{BaseUrl}/{endpoint}", queryParams);

            return new SendMessageResponse
            {
                isSuccess = response?.success ?? false,
                meessage = response?.success == true ? "success" : (response?.error ?? response?.message ?? "Unknown error")
            };
        }

        /// <inheritdoc/>
        public async Task<Whats360ProBaseResponse> GetInstancesAsync(Whats360ProSetting settings)
        {
            var queryParams = new Dictionary<string, string> { ["token"] = settings.Token };
            return await _httpClientService.GetAsync<Whats360ProBaseResponse>($"{BaseUrl}/instances", queryParams);
        }

        /// <inheritdoc/>
        public async Task<Whats360ProBaseResponse> CreateInstanceAsync(Whats360ProSetting settings, string id, string? name = null)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["token"] = settings.Token,
                ["id"] = id
            };
            if (!string.IsNullOrEmpty(name)) queryParams["name"] = name;

            return await _httpClientService.GetAsync<Whats360ProBaseResponse>($"{BaseUrl}/instances/create", queryParams);
        }

        /// <inheritdoc/>
        public async Task<Whats360ProBaseResponse> ConnectInstanceAsync(Whats360ProSetting settings)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["token"] = settings.Token,
                ["instance_id"] = settings.InstanceId
            };
            return await _httpClientService.GetAsync<Whats360ProBaseResponse>($"{BaseUrl}/instances/connect", queryParams);
        }

        /// <inheritdoc/>
        public async Task<Whats360ProBaseResponse> DisconnectInstanceAsync(Whats360ProSetting settings)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["token"] = settings.Token,
                ["instance_id"] = settings.InstanceId
            };
            return await _httpClientService.GetAsync<Whats360ProBaseResponse>($"{BaseUrl}/instances/disconnect", queryParams);
        }

        /// <inheritdoc/>
        public async Task<Whats360ProBaseResponse> DeleteInstanceAsync(Whats360ProSetting settings)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["token"] = settings.Token,
                ["instance_id"] = settings.InstanceId
            };
            return await _httpClientService.GetAsync<Whats360ProBaseResponse>($"{BaseUrl}/instances/delete", queryParams);
        }
    }
}
