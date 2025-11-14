using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebBuySource.Dto.Request.JWT;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Models.Enums;
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
        private IRepository<VerificationCode> VerificationCodeRepository => UnitOfWork.VerificationCodeRepository;

        public EmailService(IMemoryCache cache, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _config = config;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Send OTP via email (and save to DB).
        /// </summary>
        public async Task<BaseAPIResponse> SendOtp(SendOtpRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BaseApiResponse.Error(MessageConstants.EmailEmpty);

            var user = await UserRepository.GetAll().FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return BaseApiResponse.Error(MessageConstants.USER_NOT_FOUND);

            var otp = new Random().Next(100000, 999999).ToString();
            var expiryMinutes = int.Parse(Environment.GetEnvironmentVariable("OTP_EXPIRY_MINUTES") ?? "5");
            var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

            // Lưu vào DB
            var verification = new VerificationCode
            {
                Email = request.Email,
                Code = otp,
                Type = request.Type,
                UserId = user.Id,
                ExpiresAt = expiresAt,
                IsUsed = false
            };

            await VerificationCodeRepository.AddAsync(verification);
            await VerificationCodeRepository.SaveChangesAsync();

            try
            {
                var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
                var smtpPort = Environment.GetEnvironmentVariable("SMTP_PORT");
                var smtpUser = Environment.GetEnvironmentVariable("SMTP_USERNAME");
                var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
                var smtpFromEmail = Environment.GetEnvironmentVariable("SMTP_FROM_EMAIL") ?? smtpUser;
                var smtpFromName = Environment.GetEnvironmentVariable("SMTP_FROM_NAME") ?? "My App";
                var smtpUseStartTls = Environment.GetEnvironmentVariable("SMTP_USE_STARTTLS") ?? "true";

                var smtpClient = new SmtpClient(smtpHost)
                {
                    Port = int.Parse(smtpPort),
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = bool.Parse(smtpUseStartTls)
                };

                var mailMessage = EmailTemplateHelper.BuildOtpEmail(
                    smtpFromEmail, smtpFromName, request.Email, otp
                );

                mailMessage.To.Add(request.Email);
                await smtpClient.SendMailAsync(mailMessage);

                return BaseApiResponse.OK(MessageConstants.OtpSentSuccess);
            }
            catch (Exception ex)
            {
                return BaseApiResponse.Error($"{MessageConstants.OtpSendFailed}: {ex.Message}");
            }
        }

        /// <summary>
        /// Verify OTP for register or password reset.
        /// </summary>
        public async Task<BaseAPIResponse> VerifyOtp(VerifyOtpRequestDTO request)
        {
            // Validate input
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Otp))
                return BaseApiResponse.Error(MessageConstants.InvalidRequest);

            // Get the latest OTP record for this email and type
            var otpRecord = await VerificationCodeRepository.GetAll()
                .Where(v =>
                    v.Email == request.Email &&
                    v.Type == request.Type)
                .OrderByDescending(v => v.ExpiresAt)
                .FirstOrDefaultAsync();

            // No OTP record found
            if (otpRecord == null)
                return BaseApiResponse.Error(MessageConstants.OtpExpiredOrMissing);

            // OTP has already been used
            if (otpRecord.IsUsed)
                return BaseApiResponse.Error(MessageConstants.OtpAlreadyUsed);

            // OTP has expired
            if (otpRecord.ExpiresAt < DateTime.UtcNow)
                return BaseApiResponse.Error(MessageConstants.OtpExpiredOrMissing);

            // OTP does not match
            if (otpRecord.Code != request.Otp)
                return BaseApiResponse.Error(MessageConstants.OtpInvalid);

            // Mark OTP as used
            otpRecord.IsUsed = true;
            await VerificationCodeRepository.UpdateAsync(otpRecord);
            await VerificationCodeRepository.SaveChangesAsync();

            // Handle additional actions based on OTP type
            switch (request.Type)
            {
                case VerificationCodeType.REGISTER:
                    var user = await UserRepository.GetAll()
                        .FirstOrDefaultAsync(u => u.Email == request.Email);

                    if (user == null)
                        return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

                    user.IsVerified = true;
                    user.UpdatedAt = DateTime.UtcNow;

                    await UserRepository.UpdateAsync(user);
                    await UserRepository.SaveChangesAsync();
                    break;

                case VerificationCodeType.RESET_PASSWORD:
                    // No extra logic here; verification success is enough.
                    break;

                default:
                    return BaseApiResponse.Error("Invalid verification type.");
            }

            return BaseApiResponse.OK(MessageConstants.OtpVerified);
        }

    }
}
