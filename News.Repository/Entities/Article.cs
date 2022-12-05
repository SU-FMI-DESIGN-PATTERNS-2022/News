using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository.Entities
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string Summary { get; set; }
        public long Timestamp { get; set; }
        public string Url { get; set; }
        public long SourceId { get; set; }
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}
