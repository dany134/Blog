using Blog.Contracts.Repositories;
using Blog.DAL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Comments.Query
{
    public class QueryCommentsBySlug
    {
        public class Query : IRequest<IEnumerable<Comment>>
        {
            public string Slug { get; set; }
        }
        public class Handler : IRequestHandler<Query, IEnumerable<Comment>>
        {
            private readonly ICommentRepository _repository;

            public Handler(ICommentRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<Comment>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetCommentsAsync(request.Slug);
            }
        }
    }
}
