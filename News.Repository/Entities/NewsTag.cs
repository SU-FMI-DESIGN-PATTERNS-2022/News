using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository.Entities
{
    public class NewsTag
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public Article Article { get; set; }
        public long TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
