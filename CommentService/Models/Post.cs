namespace CommentService.Models
{
    // This Post model could be used in any databases like MongoDB or Redis, etc. 
    // these properties could be completely different from the post model in PostService
    // This model will be used when a message comes from our rabbitmq 
    public class Post
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
