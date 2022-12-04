using AutoMapper;
using Blog.API.DTOs;
using Blog.Contracts;
using Blog.Contracts.Services;
using Blog.DAL;
using Blog.DAL.Entities;
using Blog.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net.Mime;

namespace Blog.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _service;
        private readonly IMapper _mapper;
        public BlogPostController(IBlogPostService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string? tag)
        {
            var entites = await _service.GetBlogPostsAsync(tag);
            var mapped = _mapper.Map<IEnumerable<BlogPostDto>>(entites);
            return Ok(PostResponse<IEnumerable<BlogPostDto>>.Create(mapped, mapped.Count()));
          
        }
        [HttpGet("{slug}"), ActionName(nameof(GetPostBySlugAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostBySlugAsync(string slug)
        {
            var entity = await _service.GetPostBySlugAsync(slug);
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
            var resource = await _service.GetPostBySlugAsync(post.Slug);
            if(resource != null)
            {
                ModelState.AddModelError("Exists", $"The post with the slug {post.Slug} already exists!");
                return BadRequest(ModelState);
            }            
            var result = await _service.InsertBlogPostsAsync(post);
            if(result == true)
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
            var result = await _service.DeleteBlogPostsAsync(slug);
            if (result)
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
        public async Task<IActionResult> UpdatePostAsync(string slug, [FromBody]BlogPostForUpdateDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                var newSlug = SlugGenerator.ToUrlSlug(dto.Title);
                var exists = _service.GetPostBySlugAsync(newSlug) != null;
                if (exists)
                {
                    ModelState.AddModelError("Exists", $"The post with the slug {newSlug} already exists!");
                    return BadRequest(ModelState);
                }
            }
            
            var mapped = _mapper.Map<BlogPost>(dto);
            var result = await _service.UpdateBlogPostsAsync(slug, mapped);
            if (result)
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
