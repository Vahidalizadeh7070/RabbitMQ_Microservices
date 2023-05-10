using CommentService.Models;
using CommentService.Services.PostService.PostService;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Services.PostService
{
    public class PostServices : IPostService
    {
        private readonly AppDbContext _dbContext;

        public PostServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Post> AddPost(Post post)
        {
            if(post is not null)
            {
                await _dbContext.Posts.AddAsync(post);
                await _dbContext.SaveChangesAsync();
            }
            return post;
        }

        public async Task<Post> GetPostById(string Id)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(p=>p.Id == Id);
        }
    }
}
