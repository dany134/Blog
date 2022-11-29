using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsAsync(string slug);
        Task<bool> InsertCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(string slug);  
    }
}
