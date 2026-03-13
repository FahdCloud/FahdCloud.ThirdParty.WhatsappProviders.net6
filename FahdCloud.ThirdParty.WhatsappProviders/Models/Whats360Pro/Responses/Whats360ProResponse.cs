namespace FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Pro.Responses
{
    /// <summary>
    /// Represents a generic response from the Whats360 Pro API.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    public class Whats360ProResponse<T>
    {
        /// <summary>
        /// Indicates whether the request was successful.
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// A message describing the result of the request.
        /// </summary>
        public string? message { get; set; }

        /// <summary>
        /// An error message, if the request failed.
        /// </summary>
        public string? error { get; set; }

        /// <summary>
        /// The data returned by the API, if any.
        /// </summary>
        public T? response { get; set; }
    }

    /// <summary>
    /// Represents a basic response from the Whats360 Pro API without additional data.
    /// </summary>
    public class Whats360ProBaseResponse
    {
        /// <summary>
        /// Indicates whether the request was successful.
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// A message describing the result of the request.
        /// </summary>
        public string? message { get; set; }

        /// <summary>
        /// An error message, if the request failed.
        /// </summary>
        public string? error { get; set; }
    }
}
