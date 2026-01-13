namespace WebBuySource.Models
{
    public class UserFavorites
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int CodeId { get; set; }
        public Code Code { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
