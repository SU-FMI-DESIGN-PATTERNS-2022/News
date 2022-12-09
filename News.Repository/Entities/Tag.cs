using System.ComponentModel.DataAnnotations;

namespace News.Repository.Entities
{
    public class Tag
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
