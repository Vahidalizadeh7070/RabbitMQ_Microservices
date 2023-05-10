using CommentService.Models;
using CommentService.Services.CommentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Comment comment)
        {
            // Check post exist inside the post table
            var exist = await _commentService.ExistPost(comment.PostId);
            if(exist is not null)
            {
                var result = await _commentService.AddComment(comment);
                if (result is not null)
                {
                    return Ok(result);
                }
            }

            return BadRequest();
        }
    }
}
