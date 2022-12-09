using System.ComponentModel.DataAnnotations;

namespace News.Repository.Entities
{
    public class Article
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string Summary { get; set; }
        public long Timestamp { get; set; }
        public string Url { get; set; }
        public Source Source { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
