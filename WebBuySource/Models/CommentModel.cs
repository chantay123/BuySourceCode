using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Comment:BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CodeId { get; set; }
        public Code Code { get; set; }

        [Required]
        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        public int? ParentId { get; set; }
        public Comment? Parent { get; set; }

        [Required]
        public int Rating { get; set; } 

        [Required]
        public string CommentText { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}
