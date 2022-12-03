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
    public class QueryPostsBySlug
    {
        public class Query : IRequest<BlogPost> 
        {
            public string Slug { get; set; }
        }
        public class Handler : IRequestHandler<Query, BlogPost>
        {
            private readonly IBlogPostRepository _repository;
            public Handler(IBlogPostRepository repository)
            {
                _repository = repository;
            }
            public async Task<BlogPost> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetPostBySlugAsync(request.Slug);
              
            }
        }
    }
}
