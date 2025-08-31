
namespace FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared
{
    public interface IHttpClientService
    {
        Task<T> PostAsync<T>(string url, object data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        Task<(T response, int status)> PostWithStatusAsync<T>(string url, object data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);

        Task<T> PostFormDataAsync<T>(string url, Dictionary<string, string> formData, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);
        Task<T> GetAsync<T>(string url, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);
        Task<bool> CheckConnectionAsync(string url, object data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);
    }
}