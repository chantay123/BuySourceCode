namespace WebBuySource.Dto.Response.CommentResponse
{
    public class CommentResponseDTO : BaseAPIResponse
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string CommentText { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
