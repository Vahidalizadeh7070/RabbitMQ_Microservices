using PostService.Models;

namespace PostService.Services.PostService
{
    public class PostServices : IPostService
    {
        private readonly AppDbContext _dbContext;

        public PostServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Post> Add(Post post)
        {
            if(post is not null)
            {
                await _dbContext.Posts.AddAsync(post);
                await _dbContext.SaveChangesAsync();
            }
            return post;
        }


        public Post GetById(string id)
        {
            return _dbContext.Posts.FirstOrDefault(p => p.Id == id);
        }
    }
}
