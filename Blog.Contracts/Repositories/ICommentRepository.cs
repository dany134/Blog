using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsAsync(string slug);
        Task<bool> DeleteByIdAsync(string slug, int Id);
        Task<bool> RepositorySave();
        Task<bool> InsertComment(Comment comment);
    }
}
