namespace OnlineQuizSystem.Utilities;

public static class EmailTemplates
{
    public static string GetVerificationEmailBody(string userName, string verificationLink)
    {
        return $@"
        <html>
        <body>
            <h2>Hello {userName},</h2>
            <p>Thank you for registering with Quizzy! Please verify your email address by clicking the link below:</p>
            <a href='{verificationLink}'>Verify Email</a>
            <p>If you did not sign up for this account, please ignore this email.</p>
            <br/>
            <p>Best regards,<br/>The Quizzy Team</p>
        </body>
        </html>";
    }

    public static string GetPasswordResetEmailBody(string OTP)
    {
        return $@"
        <html>
        <body>
            <h2>Hello,</h2>
            <p>We received a request to reset your password. Use the OTP below to reset your password:</p>
            <h3>{OTP}</h3>
            <p>If you did not request a password reset, please ignore this email.</p>
            <br/>
            <p>Best regards,<br/>The Quizzy Team</p>
        </body>
        </html>";
        

    }
    
}