using AutoMapper;
using Blog.API.DTOs;
using Blog.Contracts.Services;
using Blog.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/posts/{slug}/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        private readonly IMapper _mapper;
        public CommentController(ICommentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCommentAsync(string slug)
        {
            var comments = await _service.GetCommentsAsync(slug);
            if(comments != null && comments.Any())
            {
                return Ok(_mapper.Map<IEnumerable<CommentDto>>(comments));

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertComment(string slug,[FromBody] CommentForCreationDto dto) 
        {
            var entity = _mapper.Map<Comment>(dto);
            entity.Slug = slug;
            var result = await _service.InsertCommentAsync(entity);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else 
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentAsync(string slug) 
        {
            var result = await _service.DeleteCommentAsync(slug);
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
