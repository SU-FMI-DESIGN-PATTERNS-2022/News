using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace News.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class TestController
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("TestGet")]
        public async Task<String> TestGet()
        {
            return "Success";
        }

        
    }
}
