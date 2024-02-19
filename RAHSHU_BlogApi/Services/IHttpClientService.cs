namespace RAHSHU_BlogApi.Services
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string baseAddress, string endpoint);
    }
}
