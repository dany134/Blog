using Blog.Contracts;
using Blog.DAL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Posts.Query
{
    public class QueryAllPosts 
    {
        public class Query : IRequest<IEnumerable<BlogPost>>
        {
            public string Tag { get; set; }
        }
        public class Handler : IRequestHandler<Query, IEnumerable<BlogPost>>
        {
            private readonly IBlogPostRepository _repository;
            public Handler(IBlogPostRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<BlogPost>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetPostsAsync(request.Tag);

            }
        }
    }
    
}
