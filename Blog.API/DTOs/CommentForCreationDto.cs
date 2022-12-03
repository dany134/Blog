using System.ComponentModel.DataAnnotations;

namespace Blog.API.DTOs
{
    public class CommentForCreationDto
    {
        [Required]
        public string Body { get; set; }

    }
}
