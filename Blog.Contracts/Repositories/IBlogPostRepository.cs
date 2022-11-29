using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetPostsAsync();
        Task<bool> InsertPostAsync(BlogPost blogPost);
        Task<BlogPost> GetPostBySlugAsync(string slug);
        Task<bool> DeletePostAsync(string slug);
    }
}
