using Microsoft.EntityFrameworkCore;
using News.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository.Context
{
    public class NewsDbContext : DbContext
    {

        public DbSet<Article> Articles { get; set; }
        public DbSet<Interest> Interest { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NewsTag> NewsTags {get; set;}


    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
    {

    }



}
}
