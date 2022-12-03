using Blog.Contracts;
using Blog.DAL.Entities;
using Blog.Service.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Blog.Service.Posts.Command
{
    public class UpdatePost
    {
        public class Command : IRequest<Result<Unit>>
        {
            public BlogPost Post{ get; set; }
            public string Slug { get; set; }
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
                var entity = await  _repository.GetPostBySlugAsync(request.Slug);
                if(entity != null) 
                {

                    if (!string.IsNullOrWhiteSpace(request.Post.Title))
                    {
                        if (entity.Title != request.Post.Title)
                        {
                            entity.Title = request.Post.Title.Trim();
                            entity.Slug = SlugGenerator.ToUrlSlug(request.Post.Title);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Post.Description))
                    {
                        entity.Description = request.Post.Description;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Post.Body))
                    {
                        entity.Body = request.Post.Body;
                    }
                    entity.UpdatedAt = DateTime.Now;
                    var result = await _repository.RepositorySave();
                    if (!result)
                    {
                        return Result<Unit>.Failure("No resource found in the db");

                    }
                    return Result<Unit>.Success(Unit.Value);

                }
                return Result<Unit>.Failure("No resource found in the db");
            }

        }
    }
}
