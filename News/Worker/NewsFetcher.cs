using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using AutoMapper;
using ApiArticle = NewsAPI.Models.Article;
using EntityArticle = News.Repository.Entities.Article;
using EntitySource = News.Repository.Entities.Source;
using News.Repository.Contracts;

namespace News.Worker
{
    public class NewsFetcher: BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        const int NEWS_HOURLY_UPDATE_RATE = 10;
        private readonly ILogger<NewsFetcher> _logger;
        private readonly IMapper _mapper;
        private readonly NewsApiClient newsApiClient;
        private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(NEWS_HOURLY_UPDATE_RATE));

        public NewsFetcher(IServiceScopeFactory serviceScopeFactory, ILogger<NewsFetcher> logger,
            IMapper mapper, IConfiguration configuration)
        {
            string apiKey = configuration.GetValue<string>("ApiKey");
            newsApiClient = new NewsApiClient(apiKey);
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Fetched news at " 
                        + DateTime.Now.ToString());
                    FetchNewsAsync();
                }
                catch (Exception e)
                {
                    _logger.LogInformation("Problem occured when retrieving news at "
                        + DateTime.Now.ToString() +$"with message {e.Message}");
                    throw;
                }
            }
        }

        private async void FetchNewsAsync()
        {
            var articlesResponse =await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q= "Apple",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = DateTime.Now.AddMonths(-1)
            }) ;

            if (articlesResponse.Status == Statuses.Ok)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _newsRepo = scope.ServiceProvider.GetRequiredService<INewsRepository>();
                    
                    for (int i = 0; i < articlesResponse.Articles.Count; i++)
                    {
                        ApiArticle current = articlesResponse.Articles[i];

                        EntitySource source = _mapper.Map<EntitySource>(current.Source);
                        EntityArticle article = _mapper.Map<EntityArticle>(articlesResponse.Articles[i]);
                        source.Articles.Add(article);

                        EntitySource existingSource = _newsRepo.GetOnlySources().Where(s => s.Name == source.Name).FirstOrDefault();
                        if (existingSource==null)
                        {
                            _newsRepo.AddSource(source);
                        }
                        else
                        {
                            _newsRepo.UpdateSource(existingSource);
                        }
                        _newsRepo.AddArticle(article);
                    }
                }
            }
        }
    }
}
