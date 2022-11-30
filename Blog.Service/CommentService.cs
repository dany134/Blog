using Blog.Contracts.Repositories;
using Blog.Contracts.Services;
using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        public CommentService(ICommentRepository repository)
        {
            _repository= repository;
        }
        public async Task<bool> DeleteCommentAsync(string slug, int Id)
        {
            return await _repository.DeleteByIdAsync(slug, Id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(string slug)
        {
            return await _repository.GetCommentsAsync(slug);
        }

        public Task<bool> InsertCommentAsync(Comment comment)
        {
            SetDates(comment);
            return _repository.InsertComment(comment);
        }
        private void SetDates(Comment comment) 
        {
            if(comment.CreatedAt == DateTime.MinValue)
            {
                comment.CreatedAt = DateTime.Now;
            }
            comment.UpdatedAt = DateTime.Now;
        }
    }
}
