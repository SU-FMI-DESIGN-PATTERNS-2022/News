using News.Repository;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using AutoMapper;
using ApiArticle = NewsAPI.Models.Article;
using EntityArticle = News.Repository.Entities.Article;
using News.Repository.Contracts;

namespace News.Worker
{
    public class NewsWorker: BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        const int NEWS_HOURLY_UPDATE_RATE = 1;
        private readonly ILogger<NewsWorker> _logger;
        private NewsApiClient newsApiClient;
        private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromHours(NEWS_HOURLY_UPDATE_RATE));

        public NewsWorker(IServiceScopeFactory serviceScopeFactory, ILogger<NewsWorker> logger)
        {
            string apiKey = "06833835960e41818a99eedcb231e43c";
            newsApiClient = new NewsApiClient(apiKey);
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Fetched news at " + DateTime.Now.ToString());
                await FetchNewsAsync();
            }
        }

        private async Task FetchNewsAsync()
        {
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = DateTime.Now.AddMonths(-1) // unpaid API users can't fetch data older than a month;
            }) ;

            if (articlesResponse.Status == Statuses.Ok)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ApiArticle, EntityArticle>();
                });
                var mapper = config.CreateMapper();

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _newsRepo = scope.ServiceProvider.GetRequiredService<INewsRepository>();
                    for (int i = 0; i < articlesResponse.Articles.Count; i++)
                    {
                        EntityArticle article = mapper.Map<EntityArticle>(articlesResponse.Articles[i]);
                        _newsRepo.AddArticle(article);
                    }
                }
            }
        }
    }
}
