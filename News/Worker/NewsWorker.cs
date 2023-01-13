using Microsoft.Extensions.DependencyInjection;
using News.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Worker
{
    public class NewsWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NewsWorker> _logger;

        public NewsWorker(ILogger<NewsWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    INewsService newsService =
                        scope.ServiceProvider.GetRequiredService<INewsService>();

                    newsService.FetchNewsFromExternalApi();
                    // TODO: parse the news
                    // TODO: send the news to the database
                }
            }
        }
    }
}
