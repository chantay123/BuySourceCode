using System.ComponentModel.DataAnnotations;
using WebBuySource.Models.Enums;

namespace WebBuySource.Models
{
    public class Conversation:BaseModel
    {
        [Key]
        public int Id { get; set; }

        public int? BuyerId { get; set; }
        public User? Buyer { get; set; }

        public int? AdminId { get; set; }
        public User? Admin { get; set; }

        public int? CodeId { get; set; }
        public Code? Code { get; set; }

        public ConversationStatus Status { get; set; }

        public DateTime? LastMessageAt { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}