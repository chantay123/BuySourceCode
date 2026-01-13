using System.Net.Mail;

namespace WebBuySource.Utilities.Helpers
{
    public static class EmailTemplateHelper
    {
        /// <summary>
        /// Build a beautiful, professional HTML email for OTP verification
        /// </summary>
        public static MailMessage BuildOtpEmail(string fromEmail, string fromName, string toEmail, string otp)
        {
            var subject = "🔐 Verify Your Account - One Time Password (OTP)";
            var body = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                    <title>{subject}</title>
                    <style>
                        body {{
                            margin: 0;
                            padding: 0;
                            background: #f4f6f8;
                            font-family: 'Segoe UI', Arial, Helvetica, sans-serif;
                        }}
                        .email-container {{
                            max-width: 540px;
                            margin: 40px auto;
                            background: #ffffff;
                            border-radius: 12px;
                            overflow: hidden;
                            box-shadow: 0 4px 16px rgba(0,0,0,0.08);
                        }}
                        .header {{
                            background: linear-gradient(135deg, #007bff, #0056d6);
                            color: white;
                            text-align: center;
                            padding: 28px 10px;
                            font-size: 24px;
                            font-weight: 600;
                            letter-spacing: 0.5px;
                        }}
                        .body {{
                            padding: 36px 40px;
                            color: #333333;
                        }}
                        .otp-box {{
                            display: inline-block;
                            margin: 24px 0;
                            background: linear-gradient(135deg, #007bff, #00c6ff);
                            color: white;
                            font-size: 28px;
                            font-weight: bold;
                            letter-spacing: 6px;
                            padding: 14px 32px;
                            border-radius: 10px;
                            box-shadow: 0 4px 10px rgba(0,123,255,0.25);
                        }}
                        .text-small {{
                            color: #666;
                            font-size: 14px;
                            line-height: 1.6;
                        }}
                        .footer {{
                            background: #f9f9f9;
                            text-align: center;
                            padding: 20px 0;
                            font-size: 13px;
                            color: #999;
                            border-top: 1px solid #eee;
                        }}
                        @media (max-width: 600px) {{
                            .body {{ padding: 24px; }}
                            .otp-box {{ font-size: 24px; padding: 12px 24px; }}
                        }}
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <div class='header'>
                            WebBuySource Verification
                        </div>
                        <div class='body'>
                            <p style='font-size:16px;'>Hello 👋,</p>
                            <p style='font-size:16px;'>
                                Use the OTP code below to verify your account. This code will expire in <b>5 minutes</b>.
                            </p>
                            <div style='text-align:center;'>
                                <div class='otp-box'>{otp}</div>
                            </div>
                            <p class='text-small'>
                                If you didn’t request this code, you can safely ignore this email.
                            </p>
                        </div>
                        <div class='footer'>
                            &copy; {DateTime.UtcNow.Year} <b>WebBuySource</b>. All rights reserved.<br/>
                            This is an automated message — please do not reply.
                        </div>
                    </div>
                </body>
                </html>";
            var mail = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mail.To.Add(toEmail);
            return mail;
        }
    }
}
