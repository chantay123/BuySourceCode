namespace WebBuySource.Dto.Request.JWT
{
    public class LoginRequestDTO
    {

        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
