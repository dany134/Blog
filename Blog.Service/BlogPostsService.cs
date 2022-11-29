using Blog.Contracts;
using Blog.Contracts.Services;
using Blog.DAL.Entities;
using SQLitePCL;

namespace Blog.Service
{
    public class BlogPostsService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;
        public BlogPostsService(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostsAsync(string tag)
        {
            return await _repository.GetPostsAsync(tag);
        }

        public async Task<bool> InsertBlogPostsAsync(BlogPost post)
        {
            SetDates(post);
            return await _repository.InsertPostAsync(post);
        }

        public async Task<bool> UpdateBlogPostsAsync(string slug, BlogPost postUpdate)
        {
            var entity = await GetPostBySlugAsync(slug);
            if(entity != null)
            {
                if(!string.IsNullOrWhiteSpace(postUpdate.Title))
                {
                    if(entity.Title != postUpdate.Title)
                    {
                       entity.Title = postUpdate.Title;
                       entity.Slug = postUpdate.Title.Trim().ToLower().Replace(" ", "-");
                    }
                }
                if (!string.IsNullOrWhiteSpace(postUpdate.Description))
                {
                    entity.Description = postUpdate.Description;
                }
                if (!string.IsNullOrWhiteSpace(postUpdate.Body))
                {
                    entity.Body = postUpdate.Body;
                }
                entity.UpdatedAt = DateTime.Now;
                return await _repository.RepositorySave();
            }
            return false;
        }

        public async Task<bool> DeleteBlogPostsAsync(string slug)
        {
            return await _repository.DeletePostAsync(slug);
        }
        private void SetDates(BlogPost post)
        {
            if(post.CreatedAt == DateTime.MinValue)
            {
               post.CreatedAt = DateTime.Now;
            }
            post.UpdatedAt = DateTime.Now;
        }

        public async Task<BlogPost> GetPostBySlugAsync(string slug)
        {
            var entity = await _repository.GetPostBySlugAsync(slug);
            return entity;
        }
        public async Task<IEnumerable<string>> GetTagsAsync()
        {
            return await _repository.GetTagsAsync();
        }
    }
}