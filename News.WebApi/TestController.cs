using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.Repository.Contracts;
using News.Repository.Entities;

namespace News.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class TestController
    {
        private readonly ILogger<TestController> _logger;
        private readonly INewsRepository _newsRepo;

        public TestController(ILogger<TestController> logger, INewsRepository newsRepo)
        {
            _logger = logger;
            _newsRepo = newsRepo;
        }

        [HttpGet("TestGet")]
        public async Task<List<Article>> TestGet()
        {
            return _newsRepo.GetAllArticle();
        }

        [HttpPost("TestPost")]
            public async Task PostArticle([FromBody] Article article)
        {
            _newsRepo.AddArticle(article);
        }

        
    }
}
