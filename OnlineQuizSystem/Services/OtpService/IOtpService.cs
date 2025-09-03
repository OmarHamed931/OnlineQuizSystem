namespace OnlineQuizSystem.Services.OtpService;

public interface IOtpService
{
    public string GenerateOtp(string key);
    public bool ValidateOtp(string inputOtp, string key);
    
    
}