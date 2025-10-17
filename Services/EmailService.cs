using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Caching.Memory;
using WebBuySource.Dto.Request;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Utilities;
using WebBuySource.Utilities.Constants;
using WebBuySource.Utilities.Helpers;

namespace WebBuySource.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;

        public EmailService(IConfiguration config, IMemoryCache cache)
        {
            _config = config;
            _cache = cache;
        }

        public async Task<BaseAPIResponse> SendOtp(SendOtpRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BaseApiResponse.Error(MessageConstants.EmailEmpty);

            var otp = new Random().Next(100000, 999999).ToString();

            try
            {
                var smtpSection = _config.GetSection("Smtp");
                var smtpClient = new SmtpClient(smtpSection["Host"])
                {
                    Port = int.Parse(smtpSection["Port"]),
                    Credentials = new NetworkCredential(
                        smtpSection["Username"],
                        smtpSection["Password"]
                    ),
                    EnableSsl = smtpSection.GetValue<bool>("UseStartTls")
                };
                var mailMessage = EmailTemplateHelper.BuildOtpEmail(
                        smtpSection["Username"], 
                        smtpSection["FromName"],  
                        request.Email,            
                        otp                       
                 );

                mailMessage.To.Add(request.Email);
                await smtpClient.SendMailAsync(mailMessage);

                // Cache OTP for 5  minutes
                _cache.Set(request.Email, otp, TimeSpan.FromMinutes(5));

                return BaseApiResponse.OK( MessageConstants.OtpSentSuccess);
            }
            catch (Exception ex)
            {
                return BaseApiResponse.Error($"{MessageConstants.OtpSendFailed}: {ex.Message}");
            }
        }

        public Task<BaseAPIResponse> VerifyOtp(VerifyOtpRequestDTO request)
        {
            if (!_cache.TryGetValue(request.Email, out string? cachedOtp))
                return Task.FromResult(BaseApiResponse.Error(MessageConstants.OtpExpiredOrMissing));

            if (cachedOtp != request.Otp)
                return Task.FromResult(BaseApiResponse.Error(MessageConstants.OtpInvalid));

            _cache.Remove(request.Email);
            return Task.FromResult(BaseApiResponse.OK(MessageConstants.OtpVerified));
        }
    }
}
