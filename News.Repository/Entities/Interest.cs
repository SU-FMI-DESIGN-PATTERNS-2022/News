using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository.Entities
{
    public class Interest
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public HashSet<Tag> Tags {get; set;}
    }
}
