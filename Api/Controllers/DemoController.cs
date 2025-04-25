using Domain;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        [HttpGet("hello")]
        public async Task<IActionResult> GetHello()
        {
            var message = await _demoService.GetDemoMessageAsync();
            return Ok(message);
        }
    }
}