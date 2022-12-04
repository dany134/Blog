using Blog.Contracts.Repositories;
using Blog.DAL.Entities;
using Blog.Service.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Comments.Command
{
    public class CreateComment
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Comment Comment{ get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ICommentRepository _repository;

            public Handler(ICommentRepository repository)
            {
                _repository = repository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                request.Comment.CreatedAt = DateTime.Now;
                request.Comment.UpdatedAt = DateTime.Now;

                var result = await _repository.InsertComment(request.Comment);
                if (!result)
                {
                    return Result<Unit>.Failure("Faile to create a comment");
                }
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
