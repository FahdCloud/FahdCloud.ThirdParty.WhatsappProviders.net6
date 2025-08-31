using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services._00_Shared
{
    internal class HttpClientService : IHttpClientService
    {
        #region Members
        private readonly HttpClient _httpClient;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory?.CreateClient()
                          ?? throw new ArgumentNullException(nameof(httpClientFactory), "HTTP client factory cannot be null.");
        }

        #endregion


        #region Methods

        public async Task<T> GetAsync<T>(string url, Dictionary<string, string>? queryParams = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(url, "The request URL cannot be null.");

                // Build URL with query parameters if provided
                var requestUrl = BuildUrlWithQueryParams(url, queryParams);

                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl)
                {
                    Content = new StringContent(string.Empty) // Provide an empty body
                };

                // Add ContentType header to specify expected response format
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Add other headers to the request
                HandleRequestHeaders<T>(headers, request);

                var response = await _httpClient.SendAsync(request, cancellationToken);

                var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

                return JsonSerializer.Deserialize<T>(responseJson)!;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Failed to communicate with API.", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Failed to deserialize API response.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while processing the API request.", ex);
            }
        }

        public async Task<T> PostAsync<T>(string url, object data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(url, "The request URL cannot be null.");

                ArgumentNullException.ThrowIfNull(data, "The request data cannot be null.");

                var request = CreateHttpRequestContent<T>(HttpMethod.Post, url, data);

                // Add headers to the request
                HandleRequestHeaders<T>(headers, request);

                var response = await _httpClient.SendAsync(request, cancellationToken);

                var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

                return JsonSerializer.Deserialize<T>(responseJson)!;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Failed to communicate with API.", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Failed to deserialize API response.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while processing the API request.", ex);
            }
        }

        public async Task<(T response,int status)> PostWithStatusAsync<T>(string url, object data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(url, "The request URL cannot be null.");

                ArgumentNullException.ThrowIfNull(data, "The request data cannot be null.");

                var request = CreateHttpRequestContent<T>(HttpMethod.Post, url, data);

                // Add headers to the request
                HandleRequestHeaders<T>(headers, request);

                var response = await _httpClient.SendAsync(request, cancellationToken);

                var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

                return (JsonSerializer.Deserialize<T>(responseJson)!, (int)response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Failed to communicate with API.", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Failed to deserialize API response.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while processing the API request.", ex);
            }
        }

        // New method for form data
        public async Task<T> PostFormDataAsync<T>(string url, Dictionary<string, string> formData, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(url, "The request URL cannot be null.");
                ArgumentNullException.ThrowIfNull(formData, "The form data cannot be null.");

                var request = CreateFormDataRequestContent(HttpMethod.Post, url, formData);

                // Add headers to the request
                HandleRequestHeaders<T>(headers, request);

                var response = await _httpClient.SendAsync(request, cancellationToken);

                var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

                return JsonSerializer.Deserialize<T>(responseJson)!;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Failed to communicate with API.", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Failed to deserialize API response.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while processing the API request.", ex);
            }
        }

        public async Task<bool> CheckConnectionAsync(string url, object data, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(url, "The request URL cannot be null.");

                ArgumentNullException.ThrowIfNull(data, "The request data cannot be null.");

                var request = CreateHttpRequestContent<dynamic>(HttpMethod.Post, url, data);

                // Add headers to the request
                HandleRequestHeaders<dynamic>(headers, request);

                var response = await _httpClient.SendAsync(request, cancellationToken);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Failed to communicate with API.", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Failed to deserialize API response.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while processing the API request.", ex);
            }
        }

        
        #endregion
        
        #region Helper Methods

        private static HttpRequestMessage CreateFormDataRequestContent(HttpMethod httpMethod, string url, Dictionary<string, string> formData)
        {
            var content = new FormUrlEncodedContent(formData);

            var request = new HttpRequestMessage(httpMethod, url) { Content = content };

            return request;
        }

        private static HttpRequestMessage CreateHttpRequestContent<T>(HttpMethod httpMethod, string url, object data)
        {
            var json = JsonSerializer.Serialize(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(httpMethod, url) { Content = content };

            return request;
        }

        private static void HandleRequestHeaders<T>(Dictionary<string, string>? headers, HttpRequestMessage request)
        {
            if (headers == null)
                return;

            foreach (var (key, value) in headers)
            {
                if (!request.Headers.TryAddWithoutValidation(key, value))
                    Console.WriteLine($"Failed to add header {key}: {value}");
            }
        }

        private static string BuildUrlWithQueryParams(string url, Dictionary<string, string>? queryParams)
        {
            if (queryParams == null || queryParams.Count == 0)
                return url;

            var uriBuilder = new UriBuilder(url);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var (key, value) in queryParams)
                if (!string.IsNullOrWhiteSpace(key))
                    query[key] = value;

            uriBuilder.Query = query.ToString();

            return uriBuilder.ToString();
        }

        #endregion
    }
}