using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Utilities;
using WebBuySource.Utilities.Constants;

namespace WebBuySource.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly long _maxFileSize;

        public CloudinaryService()
        {
            var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
            var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");

            if (string.IsNullOrWhiteSpace(cloudName) ||
                string.IsNullOrWhiteSpace(apiKey) ||
                string.IsNullOrWhiteSpace(apiSecret))
            {
                throw new ArgumentException("Cloudinary credentials are missing in .env!");
            }

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account)
            {
                Api = { Secure = true }
            };
            _maxFileSize = 1024 * 1024 * Convert.ToInt32(Environment.GetEnvironmentVariable("MAX_AVATAR_SIZE_MB") ?? "5");
        }

        public async Task<BaseAPIResponse> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BaseApiResponse.Error(MessageConstants.NO_FILE_PROVIDED);

            // Validate file size
            var maxFileSize = 1024 * 1024 * 5; // 5MB
            if (file.Length > maxFileSize)
                return BaseApiResponse.Error("File size must be <= 5MB");

            // Validate extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return BaseApiResponse.Error("Only JPG/JPEG/PNG files are allowed");

            try
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "users/avatar"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return BaseApiResponse.OK(
                    new { Url = uploadResult.SecureUrl.ToString() },
                    MessageConstants.IMAGE_UPLOADED_SUCCESS
                );
            }
            catch (Exception ex)
            {
                return BaseApiResponse.Error($"Upload failed: {ex.Message}");
            }

        }
    }
}
