using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using News.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository
{
    public class NewsRepository
    {
        private readonly ILogger<NewsRepository> _logger;

        public NewsRepository(ILogger<NewsRepository> logger, NewsDbContext)
        {
            _logger = logger;
        }

    }
}
