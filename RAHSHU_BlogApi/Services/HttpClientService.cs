using Newtonsoft.Json;

namespace RAHSHU_BlogApi.Services
{
    public class HttpClientService : IHttpClientService
    {
        public async Task<T> GetAsync<T>(string baseAddress, string endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                using (HttpResponseMessage response = await client.GetAsync(endpoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(responseContent);
                    }
                }
            }
            return default;
        }
    }
}
