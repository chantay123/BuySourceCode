using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebBuySource.Dto.Request;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Uow;
using WebBuySource.Utilities;
using WebBuySource.Utilities.Constants;
using WebBuySource.Utilities.Helpers;


namespace WebBuySource.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork UnitOfWork;
        private IRepository<User> UserRepository => UnitOfWork.UserRepository;

        public EmailService(IMemoryCache cache, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _cache = cache;
            UnitOfWork = unitOfWork;

        }

        /// <summary>
        /// Sends a one-time password (OTP) to the user's email address.
        /// </summary>
        public async Task<BaseAPIResponse> SendOtp(SendOtpRequestDTO request)
        {
            //  Validate input
            if (string.IsNullOrEmpty(request.Email))
                return BaseApiResponse.Error(MessageConstants.EmailEmpty);

            // Check if OTP already exists in cache (still valid)
            if (_cache.TryGetValue(request.Email, out string? existingOtp))
            {
                return BaseApiResponse.Error(MessageConstants.OtpStillValid);
            }

            // Generate a random 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();

            try
            {
                // 4️⃣ Load SMTP settings from environment variables
                var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
                var smtpPort = Environment.GetEnvironmentVariable("SMTP_PORT");
                var smtpUser = Environment.GetEnvironmentVariable("SMTP_USERNAME");
                var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
                var smtpFromName = Environment.GetEnvironmentVariable("SMTP_FROM_NAME") ?? "My App";
                var smtpFromEmail = Environment.GetEnvironmentVariable("SMTP_FROM_EMAIL");
                var smtpUseStartTls = Environment.GetEnvironmentVariable("SMTP_USE_STARTTLS") ?? "true";

                // 5️⃣ Check if any essential SMTP settings are missing
                if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpPort) ||
                    string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass))
                {
                    return BaseApiResponse.Error(MessageConstants.SmtpConfigMissing);
                }

                // 6️⃣ Initialize SMTP client
                var smtpClient = new SmtpClient(smtpHost)
                {
                    Port = int.Parse(smtpPort),
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = bool.Parse(smtpUseStartTls)
                };

                //  Build email message using helper template
                var mailMessage = EmailTemplateHelper.BuildOtpEmail(
                    smtpFromEmail ?? smtpUser, // From address
                    smtpFromName,              // Display name
                    request.Email,             // To address
                    otp                        // OTP code
                );

                // Send the email asynchronously
                mailMessage.To.Add(request.Email);
                await smtpClient.SendMailAsync(mailMessage);

                // Read OTP expiry time from environment variables (default 5 minutes)
                var expiry = int.Parse(Environment.GetEnvironmentVariable("OTP_EXPIRY_MINUTES") ?? "5");

                // Cache the OTP temporarily for later verification
                _cache.Set(request.Email, otp, TimeSpan.FromMinutes(expiry));

                return BaseApiResponse.OK(MessageConstants.OtpSentSuccess);
            }
            catch (Exception ex)
            {
                //Handle any SMTP or network-related errors
                return BaseApiResponse.Error($"{MessageConstants.OtpSendFailed}: {ex.Message}");
            }
        }


        /// <summary>
        /// Verifies that the user-provided OTP matches the cached value.
        /// </summary>
        public async Task<BaseAPIResponse> VerifyOtp(VerifyOtpRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Otp))
                return BaseApiResponse.Error(MessageConstants.InvalidRequest);

            // Try to retrieve the OTP from cache
            if (!_cache.TryGetValue(request.Email, out string? cachedOtp))
                return BaseApiResponse.Error(MessageConstants.OtpExpiredOrMissing);

            // Compare provided OTP with cached one
            if (cachedOtp != request.Otp)
                return BaseApiResponse.Error(MessageConstants.OtpInvalid);

            // Remove OTP from cache once it's successfully verified
            _cache.Remove(request.Email);

            // 🔹 Update IsVerified = true in database
            var user = await UserRepository.GetAll()
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

            user.IsVerified = true;
            user.UpdatedAt = DateTime.UtcNow;

            await UserRepository.UpdateAsync(user);
            await UserRepository.SaveChangesAsync();

            return BaseApiResponse.OK(MessageConstants.OtpVerified);
        }


    }
}
