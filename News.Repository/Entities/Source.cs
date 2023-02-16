using System.ComponentModel.DataAnnotations;

namespace News.Repository.Entities
{
    public class Source
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }

        public Source()
        {
            Articles = new HashSet<Article>();
        }
    }
}
