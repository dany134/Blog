using Blog.Contracts;
using Blog.DAL;
using Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogContext _context;
        public BlogPostRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> GetPostBySlugAsync(string slug)
        {

            return await _context.BlogPosts.FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<IEnumerable<BlogPost>> GetPostsAsync(string tag) 
        {
            var posts =  _context.BlogPosts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(tag))
            {
                posts = posts.Where(x => x.Tags.ToLower().Contains(tag.ToLower()));
            }
            posts = posts.Where(x => x.UpdatedAt >= DateTime.Now.Date.AddDays(-2));
            return await posts.ToListAsync();
        }

        public async Task<bool> InsertPostAsync(BlogPost blogPost)
        {
           if(blogPost != null)
            {
                var existing = _context.BlogPosts.Any(x => x.Slug == blogPost.Slug);
                if (existing)
                {
                    return false;
                }
                _context.BlogPosts.Add(blogPost);
                return await RepositorySave();
            }
            return false;
        }
        public async Task<bool> DeletePostAsync(string slug)
        {
            var entity = await GetPostBySlugAsync(slug);
            
            if(entity == null)
            {
                return false;
            }
            _context.BlogPosts.Remove(entity);
            return await RepositorySave();
        }
        public async Task<bool> RepositorySave()
        {
           var result = await _context.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<List<string>> GetTagsAsync()
        {
            var tags = await _context.BlogPosts.Select(x => x.Tags).ToListAsync();
            var list = new List<string>();
            foreach (var item in tags)
            {
                var split = item.Split(",");
                list.AddRange(split);
            }
            return list;
        }
    }
}
