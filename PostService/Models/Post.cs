namespace PostService.Models
{
    public class Post
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
