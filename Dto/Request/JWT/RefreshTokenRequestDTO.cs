namespace WebBuySource.Dto.Request.JWT
{
    public class RefreshTokenRequestDTO
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
