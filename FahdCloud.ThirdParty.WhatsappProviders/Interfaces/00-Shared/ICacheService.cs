
namespace FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared
{
    public interface ICacheService
    {
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> fetchDataFunction, TimeSpan? absoluteExpiration = null);
        T? GetData<T>(string key);
        T SetData<T>(string key, T data, TimeSpan? absoluteExpiration = null);
        void RemoveData(string key);
    }
}