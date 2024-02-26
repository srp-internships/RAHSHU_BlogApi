using Microsoft.AspNetCore.Mvc;
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
            try
            {
                await _service.Seed();
                return Ok("Data seeding completed successfully.");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while seeding data. Please try again later.");
            }
        }
    }
}
