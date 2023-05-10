namespace CommentService.Models
{
    public class Comment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string PostId { get; set; }
        public Post Post { get; set; }
        public string Comments { get; set; }
        public string Email { get; set; }

    }
}
