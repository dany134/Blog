using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Comment : BaseEntity
    {
        public string Body { get; set; }
    }
}
