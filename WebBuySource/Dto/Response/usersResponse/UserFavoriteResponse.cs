namespace WebBuySource.Dto.Response.usersResponse
{
    public class UserFavoriteResponse
    {
        public int CodeId { get; set; } 

        public string Name { get; set; }

        public string Title { get; set; }

        public decimal? Price { get; set; }

        public string? Description { get; set; }
        
        public int?  Quanlity { get; set; }

        public string? ThumbnailUrl { get; set; }

        public int TotalLikes { get; set; } 
        public bool IsLiked { get; set; }
    }
}
