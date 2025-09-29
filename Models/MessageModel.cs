using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Message:BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }

        [Required]
        public int SenderId { get; set; }
        public User Sender { get; set; }

        public string? MessageText { get; set; }

        public string? AttachmentUrls { get; set; }

    }
}
