using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository.Entities
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; } = new HashSet<Article>();
    }
}
