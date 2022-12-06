using System.ComponentModel.DataAnnotations;

namespace News.Repository.Entities
{
    public class Interest
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public Tag Tag {get; set;}
    }
}
