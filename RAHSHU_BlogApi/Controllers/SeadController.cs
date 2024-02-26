using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAHSHU_BlogApi.Services.SeedService;

namespace RAHSHU_BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {

        private readonly ISeedService _service;

        public SeedController(ISeedService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Seed()
        {

           await _service.Seed();


            return NoContent();
        }
    }
}
