using Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/demo")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDemoRecord([FromBody][Required] CreateDemoRecordRequest request)
        {
            await _demoService.SaveDemoMessageAsync(request.Message);
            return Ok();
        }
    }
}