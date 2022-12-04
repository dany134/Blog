using AutoMapper;
using Blog.API.DTOs;
using Blog.Contracts.Services;
using Blog.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Xml.Linq;

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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertComment(string slug, [FromBody] CommentForCreationDto dto) 
        {
            var entity = _mapper.Map<Comment>(dto);
            entity.Slug = slug;
            var result = await _service.InsertCommentAsync(entity);
            if (result)
            {
                return Created($"posts/{entity.Slug}/comments/{entity.Id}", _mapper.Map<CommentDto>(entity));
            }
            else 
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCommentAsync(string slug, int id) 
        {
            var result = await _service.DeleteCommentAsync(slug, id);
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
