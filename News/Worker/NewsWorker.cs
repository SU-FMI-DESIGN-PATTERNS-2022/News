using News.Repository;
using News.Repository.Context;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using News.Repository.Entities;

namespace News.Worker
{
    public class NewsWorker: BackgroundService
    {
        const int NEWS_HOURLY_UPDATE_RATE = 1;
        private readonly ILogger<NewsWorker> _logger;
        private NewsApiClient newsApiClient;
        private readonly NewsDbContext _dbContext;
        private readonly NewsRepository _newsRepo;
        private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromHours(NEWS_HOURLY_UPDATE_RATE));

        public NewsWorker(ILogger<NewsWorker> logger, NewsDbContext dbContext, NewsRepository newsRepo)
        {
            string apiKey = "06833835960e41818a99eedcb231e43c";
            newsApiClient = new NewsApiClient(apiKey);
            _logger = logger;
            _dbContext = dbContext;
            _newsRepo = newsRepo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
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
                for (int i = 0; i < articlesResponse.Articles.Count; i++)
                {
                    NewsAPI.Models.Article article = articlesResponse.Articles[i];
                    _newsRepo.AddArticle(
                        new Repository.Entities.Article()
                        {
                           Title = article.Title,
                           Summary = article.Description,
                           Source= new Repository.Entities.Source() {Name = article.Source.Name}, //TODO: add article
                           Picture = article.UrlToImage,
                           Url = article.Url,
                           Timestamp= //is this long supposed to be ticks or?
                           Tags = article. // no tags in request ? 
                        });
                }
            }
        }
    }
}
