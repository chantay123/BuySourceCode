namespace WebBuySource.Utilities.Constants
{
    public static class MessageConstants
    {
        // Common
        public const string SUCCESS = "Success";
        public const string FAILED = "Failed";
        public const string VALIDATION_ERROR = "Invalid data";
        public const string InvalidRequest = "Invalid request data.";

        // Auth
        public const string EMAIL_NOT_VERIFIED = "Your email has not been verified. Please verify before logging in.";
        public const string USER_DELETED_SUCCESS = "User deleted successfully";
        public const string USER_UPDATED_SUCCESS = "User updated successfully";
        public const string USER_NOT_FOUND = "User not found";
        public const string INVALID_PASSWORD = "Invalid password";
        public const string LOGIN_SUCCESS = "Login successful";
        public const string REGISTER_SUCCESS = "Registration successful";
        public const string EMAIL_EXISTS = "Email already exists";
        public const string PASSWORD_NOT_MATCH = "Passwords do not match";
        public const string USERNAME_OR_EMAIL_EXISTS = "Username or email already exists";
        public const string INVALID_ACCESS_TOKEN = "Invalid access token";
        public const string REFRESH_TOKEN_NOT_FOUND = "No refresh token found";
        public const string INVALID_REFRESH_TOKEN = "Invalid refresh token";
        public const string ACCESS_TOKEN_EXPIRED = "Access token has expired.";
        public const string ACCESS_TOKEN_VALID = "Access token is still valid.";
        public const string NEW_ACCESS_TOKEN_ISSUED = "New access token has been issued successfully.";
        public const string LOGOUT_SUCCESS = "Logout successful.";
        public const string REFRESH_TOKEN_REQUIRED = "Refresh token is required.";
        public const string REFRESH_TOKEN_INVALID = "Invalid or already logged out.";
        public const string REFRESH_TOKEN_EXPIRED = "Refresh token has expired.";
        public const string RESET_PASSWORD_SUCCESS = "Password reset successfully.";
        //email
        public const string EMAIL_REQUIRED = "Email is required.";
        public const string EmailEmpty = "Email must not be empty.";
        public const string OtpSentSuccess = "OTP has been sent to your email.";
        public const string OtpSendFailed = "Failed to send OTP.";
        public const string OtpExpiredOrMissing = "OTP has expired or does not exist.";
        public const string OtpInvalid = "Invalid OTP code.";
        public const string OtpVerified = "OTP verification successful.";
        public const string SmtpConfigMissing = "SMTP configuration is missing from environment variables.";
        public const string OtpStillValid = "An OTP has already been sent and is still valid. Please wait until it expires.";
        public const string OtpAlreadyUsed = "Mã OTP này đã được sử dụng.";
        // Category
        public const string CATEGORY_NOT_FOUND = "Category not found";
        public const string INVALID_CATEGORY_ID = "Invalid category ID provided";

        /// Environment Messages
        public const string MissingJwtKey = "JWT_KEY is missing from environment variables.";
        public const string MissingJwtIssuer = "JWT_ISSUER is missing from environment variables.";
        public const string MissingJwtAudience = "JWT_AUDIENCE is missing from environment variables.";
        public const string MissingJwtExpireMinutes = "JWT_EXPIRE_MINUTES is missing from environment variables.";
        public const string InvalidJwtToken = "Invalid or expired JWT token.";
        public const string JwtTokenGenerated = "JWT token generated successfully.";

        // ==== Category Messages ====
        public const string CategoryNameRequired = "Category name is required.";
        public const string CategoryCreatedSuccess = "Category created successfully.";
        public const string CategoryUpdatedSuccess = "Category updated successfully.";
        public const string CategoryDeletedSuccess = "Category deleted successfully.";
        public const string CategoryNotFound = "Category not found.";
        public const string InvalidCategoryId = "Invalid category ID.";

        ///Change password
        public const string PASSWORD_CHANGED_SUCCESS = "Your password has been changed successfully.";
        public const string PASSWORD_FIELDS_REQUIRED = "All password fields are required.";
        public const string PASSWORD_SAME_AS_OLD = "The new password cannot be the same as the current password.";
        public const string CURRENT_PASSWORD_INCORRECT = "The current password is incorrect.";

        // Cloudinary / Image upload
        public const string NO_FILE_PROVIDED = "No file provided";
        public const string IMAGE_UPLOADED_SUCCESS = "Image uploaded successfully";
    }
}
