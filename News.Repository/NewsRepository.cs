using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using News.Repository.Context;
using News.Repository.Contracts;
using News.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly ILogger<NewsRepository> _logger;
        private readonly NewsDbContext _dbContext;

        public NewsRepository(ILogger<NewsRepository> logger, NewsDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public List<Article> GetAllArticle()
        {
            List<Article> result = _dbContext.Articles
                .Include(x => x.Tags)
                .Include(x => x.Source)
                .ToList();

            return result;
        }

        public List<Source> GetAllSources()
        {
            List<Source> result = _dbContext.Sources
                .Include(x => x.Articles)
                .ToList();

            return result;
        }

        public List<Source> GetOnlySources()
        {
            List<Source> result = _dbContext.Sources
                .ToList();

            return result;
        }

        public List<Tag> GetAllTags()
        {
            List<Tag> result = _dbContext.Tags
                .Include(x => x.Users)
                .Include(x => x.Articles)
                .ToList();

            return result;
        }

        public List<Tag> GetOnlyTags()
        {
            List<Tag> result = _dbContext.Tags
                .ToList();

            return result;
        }

        public List<User> GetAllUsers()
        {
            List<User> result = _dbContext.Users
                .Include(x => x.Tags)
                .ToList();

            return result;
        }

        public void AddArticle(Article article)
        {
            try
            {
                var result = _dbContext.Articles.Add(article).Entity;
                _dbContext.SaveChanges();
                _logger.LogInformation($"Added new Article with title {result.Title}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error adding new Article with title {article.Title}");
            }
        }

        public void AddSource(Source source)
        {
            try
            {
                var result = _dbContext.Sources.Add(source).Entity;
                _dbContext.SaveChanges();
                _logger.LogInformation($"Added new Source with name {result.Name}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error adding new Source with name {source.Name}");
            }
        }

        public void AddTag(Tag tag)
        {
            try
            {
                var result = _dbContext.Tags.Add(tag).Entity;
                _dbContext.SaveChanges();
                _logger.LogInformation($"Added new Tag with name {result.Name}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error adding new Tag with name {tag.Name}");
            }
        }

        public void AddUser(User user)
        {
            try
            {
                var result = _dbContext.Users.Add(user).Entity;
                _dbContext.SaveChanges();
                _logger.LogInformation($"Added new User with username {result.Username}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error adding new User with username {user.Username}");
            }
        }

        public Article UpdateArticle(Article article)
        {
            try
            {
                var result = _dbContext.Update(article);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Updated Article with title {result.Entity.Title}");
                return result.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error updating Article with title {article.Title}");
                return null;
            }
        }

        public Source UpdateSource(Source source)
        {
            try
            {
                var result = _dbContext.Update(source);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Updated Source with name {result.Entity.Name}");
                return result.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error updating Source with name {source.Name}");
                return null;
            }
        }

        public Tag UpdateTag(Tag tag)
        {
            try
            {
                var result = _dbContext.Update(tag);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Updated Tag with name {result.Entity.Name}");
                return result.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error updating Tag with name {tag.Name}");
                return null;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                var result = _dbContext.Update(user);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Updated User with username {result.Entity.Username}");
                return result.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error updating User with username {user.Username}");
                return null;
            }
        }

        public bool DeleteArticle(long articleId)
        {
            try
            {
                var articleToBeDeleted = _dbContext.Articles.FirstOrDefault(x => x.Id == articleId);
                if (articleToBeDeleted == null)
                {
                    _logger.LogInformation($"No such Article with id {articleId} exists");
                    return false;
                }

                _dbContext.Remove(articleToBeDeleted);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Article with id {articleId} has been removed");
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error deleting Article with id {articleId}");
                return false;
            }
        }

        public bool DeleteSource(long sourceId)
        {
            try
            {
                var sourceToBeDeleted = _dbContext.Sources.FirstOrDefault(x => x.Id == sourceId);
                if (sourceToBeDeleted == null)
                {
                    _logger.LogInformation($"No such Source with id {sourceId} exists");
                    return false;
                }

                _dbContext.Remove(sourceToBeDeleted);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Source with id {sourceId} has been removed");
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error deleting Source with id {sourceId}");
                return false;
            }
        }

        public bool DeleteTag(long tagId)
        {
            try
            {
                var tagToBeDeleted = _dbContext.Tags.FirstOrDefault(x => x.Id == tagId);
                if (tagToBeDeleted == null)
                {
                    _logger.LogInformation($"No such Tag with id {tagId} exists");
                    return false;
                }

                _dbContext.Remove(tagToBeDeleted);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Tag with id {tagId} has been removed");
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error deleting Tag with id {tagId}");
                return false;
            }
        }

        public bool DeleteUser(long userId)
        {
            try
            {
                var userToBeDeleted = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
                if (userToBeDeleted == null)
                {
                    _logger.LogInformation($"No such User with id {userId} exists");
                    return false;
                }

                _dbContext.Remove(userToBeDeleted);
                _dbContext.SaveChanges();
                _logger.LogInformation($"User with id {userId} has been removed");
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error deleting User with id {userId}");
                return false;
            }
        }

    }
}
