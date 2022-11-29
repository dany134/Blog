using Blog.Contracts;
using Blog.Contracts.Services;
using Blog.DAL.Entities;

namespace Blog.Service
{
    public class BlogPostsService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;
        public BlogPostsService(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostsAsync()
        {
            return await _repository.GetPostsAsync();
        }

        public async Task<bool> InsertBlogPostsAsync(BlogPost post)
        {
            SetDates(post);
            return await _repository.InsertPostAsync(post);
        }

        public async Task UpdateBlogPostsAsync(string slug, BlogPost post)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteBlogPostsAsync(string slug)
        {
            return await _repository.DeletePostAsync(slug);
        }
        private void SetDates(BlogPost post)
        {
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
        }

        public async Task<BlogPost> GetPostBySlugAsync(string slug)
        {
            var entity = await _repository.GetPostBySlugAsync(slug);
            return entity;
        }
    }
}