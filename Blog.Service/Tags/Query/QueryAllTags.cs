using Blog.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Tags.Query
{
    public class QueryAllTags
    {

        public class Query : IRequest<IEnumerable<string>> 
        {

        }
        public class Handler : IRequestHandler<Query, IEnumerable<string>>
        {
            private readonly IBlogPostRepository _repository;

            public Handler(IBlogPostRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetTagsAsync();
            }
        }
    }
}
