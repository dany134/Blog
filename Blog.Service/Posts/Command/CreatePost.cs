using Blog.Contracts;
using Blog.DAL.Entities;
using Blog.Service.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Posts.Command
{
    public class CreatePost
    {
        public class Command : IRequest<Result<Unit>>
        {
            public BlogPost BlogPost{ get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IBlogPostRepository _repository;
            public Handler(IBlogPostRepository repository)
            {
                _repository = repository;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                request.BlogPost.CreatedAt = DateTime.Now;
                request.BlogPost.UpdatedAt= DateTime.Now;
                var result = await _repository.InsertPostAsync(request.BlogPost);
                if (result)
                {
                    return Result<Unit>.Success(Unit.Value);
                }
                else 
                {
                   return Result<Unit>.Failure("Failed to create a post") ;
                }
            }
        }
    }
}
