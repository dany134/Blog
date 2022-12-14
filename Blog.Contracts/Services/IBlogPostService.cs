using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Services
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost>> GetBlogPostsAsync(string tag);
        Task<bool> InsertBlogPostsAsync(BlogPost post);
        Task<bool> UpdateBlogPostsAsync(string slug, BlogPost post);
        Task<bool> DeleteBlogPostsAsync(string slug);
        Task<BlogPost> GetPostBySlugAsync(string slug);
        Task<IEnumerable<string>> GetTagsAsync();


    }
}
