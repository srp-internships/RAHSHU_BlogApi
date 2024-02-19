using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAHSHU_BlogApi.Services.SeedService;

namespace RAHSHU_BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {

        private readonly ISeedService service;

        public SeedController(ISeedService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Seed()
        {
           await service.Seed();


            return NoContent();
        }

        public async Task<T> GetData<T>(string baseAddress, string endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                using (HttpResponseMessage response = await client.GetAsync(endpoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        T items = JsonConvert.DeserializeObject<T>(responseContent);

                        return items;
                    }
                }
            }
            return default;
        }
    }
}
