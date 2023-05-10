using CommentService.Models;

namespace CommentService.Services.CommentService
{
    public interface ICommentService
    {
        Task<Comment> AddComment(Comment comment);
        Task<IEnumerable<Comment>> GetCommentByPostId (string postId);
        Task<Post> ExistPost(string postId);
    }
}
