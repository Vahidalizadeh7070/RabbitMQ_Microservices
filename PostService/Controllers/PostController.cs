using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.ServiceBus;
using PostService.Services.PostService;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostServiceBus _postServiceBus;

        public PostController(IPostService postService, IPostServiceBus postServiceBus)
        {
            _postService = postService;
            _postServiceBus = postServiceBus;
        }

        // Best Practice : Using DTO object to get request 
        // Best Practice : Using Mapper like AutoMapper or Mapster
        
        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            var result = await _postService.Add(post);
            if(result is not null)
            {
                _postServiceBus.PublishNewPost(result);
                return Ok(result);
            }
            return BadRequest("There is an error in your inputs");
        }
    }
}
