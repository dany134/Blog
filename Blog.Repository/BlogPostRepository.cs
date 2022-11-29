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
            return await _context.BlogPosts.FindAsync(slug);
        }

        public async Task<IEnumerable<BlogPost>> GetPostsAsync() 
        {
            var posts = await _context.BlogPosts.ToListAsync();
            return posts;
        }

        public async Task<bool> InsertPostAsync(BlogPost blogPost)
        {
           if(blogPost != null)
            {
                _context.BlogPosts.Add(blogPost);
                return await RepositorySave();
            }
            return false;
        }
        public async Task<bool> DeletePostAsync(string slug)
        {
            var entity = await _context.BlogPosts.FindAsync(slug);
            
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
    }
}
