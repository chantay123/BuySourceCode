namespace WebBuySource.Models
{
    public class CodeLike
    {
        public int UserId { get; set; }
        public int CodeId { get; set; }

        public User User { get; set; } = null!;
        public Code Code { get; set; } = null!;

        public DateTime? DateLastMant { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
