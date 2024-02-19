using System.ComponentModel.DataAnnotations;

namespace RAHSHU_BlogApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }

        public virtual List<Post> Posts { get; set; } = [];
    }
}
