namespace WebBuySource.Dto.Request
{
    public class ResetPasswordRequestDTO
    {
        public required string Email { get; set; } 
        public required string NewPassword { get; set; } 
        public required string ConfirmPassword { get; set; }
    }
}
