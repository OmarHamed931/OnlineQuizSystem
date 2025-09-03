using Microsoft.Extensions.Caching.Memory;

namespace OnlineQuizSystem.Services.OtpService;

public class OtpService : IOtpService
{
    private readonly IMemoryCache _cache;
    public OtpService(IMemoryCache cache)
    {
        _cache = cache;
    }
    public string GenerateOtp(string key)
    {
        var random = new Random();
        var otp = random.Next(100000, 999999).ToString();
        _cache.Set(key, otp, TimeSpan.FromMinutes(20)); // OTP valid for 20 minutes
        return otp;
    }
    public bool ValidateOtp(string inputOtp, string actualOtp , string key)
    {
        if(_cache.TryGetValue(key, out string? cachedOtp))
        {
            if(cachedOtp == inputOtp && cachedOtp == actualOtp)
            {
                _cache.Remove(key); // Invalidate OTP after successful validation
                return true;
            }
        }
        return false;
    }
    
    

}