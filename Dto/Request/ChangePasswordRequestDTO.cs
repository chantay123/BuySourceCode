namespace WebBuySource.Dto.Request
{
    public class ChangePasswordRequestDTO
    {

        /// <summary>
        /// The user's current password.
        /// </summary>
        public required string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// The new password the user wants to set.
        /// </summary>
        public required string NewPassword { get; set; } 

        /// <summary>
        /// Confirmation of the new password.
        /// </summary>
        public required string ConfirmPassword { get; set; } 
    }
}
