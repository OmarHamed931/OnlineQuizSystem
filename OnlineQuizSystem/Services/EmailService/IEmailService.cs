namespace OnlineQuizSystem.Services.EmailService;

public interface IEmailService
{
    public Task SendEmailAsync(string toEmail , string OTP);
    
}