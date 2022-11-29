using Blog.Contracts.Repositories;
using Blog.DAL;
using Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext context;

        public CommentRepository(BlogContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Comment>> GetCommentsAsync(string slug)
        {
            return await context.Comments.Where(x => x.Slug == slug).ToListAsync();
        }
        public async Task<bool> InsertComment(Comment comment)
        {
            if(comment  != null) 
            {
                context.Comments.Add(comment);
                return await RepositorySave();
            }
            return false;
        }
        public async Task<bool> RepositorySave()
        {
            var result = await context.SaveChangesAsync();
            return result > 0 ? true : false;
        }
        public async Task<bool> DeleteBySlugAsyn(string slug)
        {
            if (!string.IsNullOrWhiteSpace(slug))
            {
                var entites = context.Comments.Where(x => x.Slug == slug);
                context.Comments.RemoveRange(entites);
                return await RepositorySave();
            }
            return false;
        }
    }
}
