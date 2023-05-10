using PostService.Models;

namespace PostService.ServiceBus
{
    public interface IPostServiceBus
    {
        // Better using DTO object instead of Post model directly
        void PublishNewPost(Post post);
    }
}
