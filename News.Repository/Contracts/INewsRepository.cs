using News.Repository.Entities;

namespace News.Repository.Contracts
{
    public interface INewsRepository
    {
        void AddArticle(Article article);
        void AddSource(Source source);
        void AddTag(Tag tag);
        void AddUser(User user);
        bool DeleteArticle(long articleId);
        bool DeleteSource(long sourceId);
        bool DeleteTag(long tagId);
        bool DeleteUser(long userId);
        List<Article> GetAllArticle();
        List<Source> GetAllSources();
        List<Tag> GetAllTags();
        List<User> GetAllUsers();
        List<Source> GetOnlySources();
        List<Tag> GetOnlyTags();
        Article UpdateArticle(Article article);
        Source UpdateSource(Source source);
        Tag UpdateTag(Tag tag);
        User UpdateUser(User user);
    }
}