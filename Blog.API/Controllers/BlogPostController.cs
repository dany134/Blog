using AutoMapper;
using Blog.API.DTOs;
using Blog.Contracts;
using Blog.Contracts.Services;
using Blog.DAL;
using Blog.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Get(string? tag)
        {
            var entites = await _service.GetBlogPostsAsync(tag);
            var mapped = _mapper.Map<IEnumerable<BlogPostDto>>(entites);
            return Ok(mapped);
          
        }
        [HttpGet("{slug}")]
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
        public async Task<IActionResult> Post([FromBody]BlogPostForCreationDto blogPost) 
        {
            if(blogPost == null)
            {
                return BadRequest();
            }     
             var post = _mapper.Map<BlogPost>(blogPost);
            
            var result = await _service.InsertBlogPostsAsync(post);
            if(result == true)
            {
                return StatusCode(StatusCodes.Status201Created);

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpDelete("{slug}")]
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
        public async Task<IActionResult> UpdatePostAsync(string slug, [FromBody]BlogPostForUpdateDto dto)
        {
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
