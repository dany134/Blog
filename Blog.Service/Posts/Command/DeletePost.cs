using Blog.Contracts;
using Blog.Service.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Posts.Command
{
    public class DeletePost
    {
        public class Command : IRequest<Result<Unit>> 
        {
            public string Slug { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IBlogPostRepository _repositroy;
            public Handler(IBlogPostRepository repository   )
            {
                _repositroy = repository;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _repositroy.DeletePostAsync(request.Slug);
                if (!result)
                {

                    return Result<Unit>.Failure("Item not found");
                }
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
