using Microsoft.EntityFrameworkCore;
using News.Repository.Entities;

namespace News.Repository.Context
{
    public class NewsDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Article>()
                   .HasMany(x => x.Tags)
                   .WithMany(y => y.Articles)
                   .UsingEntity(j => j.ToTable("ArticleTags"));

            builder.Entity<Article>()
                   .HasOne(x => x.Source)
                   .WithMany(y => y.Articles);

            builder.Entity<User>()
                   .HasMany(x => x.Tags)
                   .WithMany(y => y.Users)
                   .UsingEntity(j => j.ToTable("Interests"));

        }
    }
}
