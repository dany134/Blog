using AutoMapper;
using Blog.API.DTOs;
using Blog.DAL.Entities;
using Blog.Service.Comments.Command;
using Blog.Service.Comments.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Blog.API.Controllers
{
    [Route("api/posts/{slug}/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
       
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CommentController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCommentAsync(string slug)
        {
            var comments = await _mediator.Send(new QueryCommentsBySlug.Query { Slug = slug });
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
        public async Task<IActionResult> InsertComment(string slug,[FromBody] CommentForCreationDto dto) 
        {
            var entity = _mapper.Map<Comment>(dto);
            entity.Slug = slug;
            var result = await _mediator.Send(new CreateComment.Command { Comment = entity });
            if (result.IsSuccess)
            {
                return Created($"{entity.Slug}/comments/{entity.Id}", entity);
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
            var result = await _mediator.Send(new DeleteComment.Command { Id = id, Slug = slug });
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
