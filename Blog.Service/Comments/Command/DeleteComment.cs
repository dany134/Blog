using Blog.Contracts.Repositories;
using Blog.Service.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Comments.Command
{
    public  class DeleteComment
    {

        public class Command : IRequest<Result<Unit>> 
        {
            public string Slug { get; set; }
            public int Id { get; set; }
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
                var result = await _repository.DeleteByIdAsync(request.Slug, request.Id);
                if (!result)
                {
                    return Result<Unit>.Failure("Not found");
                }
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
