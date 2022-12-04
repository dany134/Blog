using AutoMapper;
using Blog.API.DTOs;
using Blog.Contracts;
using Blog.DAL;
using Blog.DAL.Entities;
using Blog.Service.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Blog.Service.Posts.Query;
using Blog.Service.Posts.Command;
using System.Net.Mime;

namespace Blog.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
      
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public BlogPostController(IMapper mapper, IMediator mediator)
        {          
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogPostDto))]
        public async Task<IActionResult> Get(string? tag)
        {
            var entites = await _mediator.Send(new QueryAllPosts.Query { Tag = tag });
            var mapped = _mapper.Map<IEnumerable<BlogPostDto>>(entites);
            return Ok(PostResponse<IEnumerable<BlogPostDto>>.Create(mapped, mapped.Count()));
          
        }
        [HttpGet("{slug}"), ActionName(nameof(GetPostBySlugAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogPostDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostBySlugAsync(string slug)
        {
            var entity = await _mediator.Send(new QueryPostsBySlug.Query { Slug = slug});
            if(entity != null)
            {
                var dto = _mapper.Map<BlogPostDto>(entity);
                return Ok(dto);
            }
            return NotFound();
        }
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]BlogPostForCreationDto blogPost) 
        {
            if(blogPost == null)
            {
                return BadRequest();
            }     
            
             var post = _mapper.Map<BlogPost>(blogPost);
            var resource = await _mediator.Send(new QueryPostsBySlug.Query { Slug = post.Slug });
            if(resource != null)
            {
                ModelState.AddModelError("Exists", $"The post with the slug {post.Slug} already exists!");
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(new CreatePost.Command { BlogPost = post });
            if(result.IsSuccess == true)
            {
                return CreatedAtAction(nameof(GetPostBySlugAsync), new { slug = post.Slug }, post);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpDelete("{slug}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePostBySlugAsync(string slug)
        {
            var result = await _mediator.Send(new DeletePost.Command { Slug = slug });
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{slug}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePostAsync(string slug, [FromBody]BlogPostForUpdateDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                var newSlug = SlugGenerator.ToUrlSlug(dto.Title);
                var exists = await _mediator.Send(new QueryPostsBySlug.Query { Slug = newSlug }) != null;
                if (exists)
                {
                    ModelState.AddModelError("Exists", $"The post with the slug {newSlug} already exists!");
                    return BadRequest(ModelState);
                }
            }
            
            var mapped = _mapper.Map<BlogPost>(dto);
            var result = await _mediator.Send(new UpdatePost.Command { Slug = slug, Post = mapped });
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
           
        }
    }
}
