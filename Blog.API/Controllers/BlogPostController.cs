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
        public async Task<IEnumerable<BlogPost>> Get()
        {
            return await _service.GetBlogPostsAsync();
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
    }
}
