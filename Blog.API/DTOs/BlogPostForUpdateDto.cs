using System.Text.Json.Serialization;

namespace Blog.API.DTOs
{
    public class BlogPostForUpdateDto
    {

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public string[] TagList{ get; set; }
    }
}
