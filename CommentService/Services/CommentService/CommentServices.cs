using CommentService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Services.CommentService
{
    public class CommentServices : ICommentService
    {
        private readonly AppDbContext _dbContext;

        public CommentServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Comment> AddComment(Comment comment)
        {
            if(comment is not null)
            {
                await _dbContext.Comments.AddAsync(comment);
                await _dbContext.SaveChangesAsync();
            }
            return comment;
        }

        public async Task<Post> ExistPost(string postId)
        {
            return await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<IEnumerable<Comment>> GetCommentByPostId(string postId)
        {
            return await _dbContext.Comments.Where(c => c.PostId == postId).ToListAsync();
        }
    }
}
