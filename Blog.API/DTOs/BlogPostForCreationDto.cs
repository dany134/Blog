using System.ComponentModel.DataAnnotations;

namespace Blog.API.DTOs
{
    public class BlogPostForCreationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
   
        public string[] TagList { get; set; }
        public string Body { get; set; }
    }
}
