using System.ComponentModel.DataAnnotations;

namespace RAHSHU_BlogApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
