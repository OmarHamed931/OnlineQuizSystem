namespace OnlineQuizSystem.Services.EmailService;
using SendGrid;
using SendGrid.Helpers.Mail;
using OnlineQuizSystem.Utilities;

public class EmailService : IEmailService
{
    public string? apiKey;
    public string Emailfrom;

    public EmailService()
    {
        apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        Emailfrom = "quizzyservice@gmail.com";
    }

    public async Task SendEmailAsync(string toEmail , string OTP)
    {
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(Emailfrom, "Quizzy Service");
        var to = new EmailAddress(toEmail);
        var subject = "password reset OTP";
        var htmlContent = EmailTemplates.GetPasswordResetEmailBody(OTP);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
        var response = await client.SendEmailAsync(msg);



    }
}