using PostService.Models;

namespace PostService.Services.PostService
{
    public interface IPostService
    {
        Task<Post> Add(Post post);
        Post GetById(string id);
    }
}
