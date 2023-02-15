using NewsAPI.Constants;
using NewsAPI.Models;
using AutoMapper;
using ApiArticle = NewsAPI.Models.Article;
using EntityArticle = News.Repository.Entities.Article;
using News.Repository.Contracts;

namespace News.Worker
{
    public class NewsDeleter : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        const int NEWS_DAYS_DELETION_RATE = 90; // data older than 3 months will be deleted
        private readonly ILogger<NewsFetcher> _logger;
        private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromDays(NEWS_DAYS_DELETION_RATE));

        public NewsDeleter(IServiceScopeFactory serviceScopeFactory, ILogger<NewsFetcher> logger)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Deleted news at " + DateTime.Now.ToString());
                    DeleteNewsAsync();
                }
                catch (Exception e)
                {
                    _logger.LogInformation("Problem occured when deleting news at " 
                        + DateTime.Now.ToString() +$"with message: {e.Message}");
                    throw;
                }
            }
        }

        private async void DeleteNewsAsync()
        {
            using (var scope = _serviceScopeFactory.CreateAsyncScope())
            {
                var _newsRepo = scope.ServiceProvider.GetRequiredService<INewsRepository>();
                var _articlesToDelete = _newsRepo.GetAllArticle().Where(a=> OlderThan(new DateTime(a.Timestamp),3));
                for (int i = 0; i < _articlesToDelete.Count(); i++)
                {
                    await Task.Run(() => _newsRepo.DeleteArticle(_articlesToDelete.ElementAt(i).Id));
                }
            }
        }

        private bool OlderThan(DateTime retrievalDate,int months)
        {
            return retrievalDate < DateTime.Now.Date.AddMonths(-months);
        }
    }
}
