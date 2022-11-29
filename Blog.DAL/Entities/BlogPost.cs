using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class BlogPost : BaseEntity
    {
       
        public string Title { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

    }
}
