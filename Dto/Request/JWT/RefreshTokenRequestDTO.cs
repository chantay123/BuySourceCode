namespace WebBuySource.Dto.Request.JWT
{
    public class RefreshTokenRequestDTO
    {
        public required string accessToken { get; set; }
        public required string refreshToken { get; set; }
    }
}
