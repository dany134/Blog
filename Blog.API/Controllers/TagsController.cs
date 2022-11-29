using Blog.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IBlogPostService _service;

        public TagsController(IBlogPostService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetTagsAsync()
        {
            return Ok(await _service.GetTagsAsync());
        }
    }
}
