using System.Text;
using GenerativeAI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using OnlineQuizSystem.Models;
using OnlineQuizSystem.Services.AuthService;
using OnlineQuizSystem.Services.JWTService;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Repositories.QuestionRepo;
using OnlineQuizSystem.Repositories.UserRepo;
using OnlineQuizSystem.Services.AIService;
using OnlineQuizSystem.Services.EmailService;
using OnlineQuizSystem.Services.OtpService;
using OnlineQuizSystem.Services.QuestionService;
using OnlineQuizSystem.Utilities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//database exception filter

// Authentication and Authorization services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

// add IconfigurationBuilder to TokenService
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSingleton<IConfigurationBuilder>(builder.Configuration);

// Questions services

builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionRepo, QuestionRepo>();

// AI services
builder.Services.AddSingleton<IAIService, AIService>();

// Email services
builder.Services.AddSingleton<IEmailService, EmailService>();

// Otp services
builder.Services.AddSingleton<IOtpService, OtpService>();

// Memory cache
builder.Services.AddMemoryCache();

// logger service
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);




// AI service test 

/*
var apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
var googleAI = new GoogleAi(apiKey);
var googleModel = googleAI.CreateGenerativeModel("models/gemini-1.5-flash");
string Text = "What is the boiling point of water at sea level?";    
string CorrectAnswer = "100°C";
string submittedAnswer = "It boils";
var prompt = $"Question: {Text}\nCorrect Answer: {CorrectAnswer}\nSubmitted Answer: {submittedAnswer}\nIs the submitted answer correct? Answer with 'true' or 'false' with explanation.";
var response = await googleModel.GenerateObjectAsync<AnswerDTOs.ShortAnswerDTO>(prompt);
Console.WriteLine($"AI Response: {response.IsCorrect}, Confidence: {response.Confidence}, explanation: {response.explanation}");
Console.WriteLine(response.Confidence);
*/




builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // will be set in environment variables
        };
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        context.Database.Migrate();
        logger.LogInformation("Database migrated successfully.");
        await Seeder.SeedCategoriesAsync(context);
        await Seeder.SeedQuestionsAsync(context);
    }
    catch (Exception ex)
    {
        
        logger.LogError(ex, "An error occurred while migrating the database.");
    }

    
}



app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Welcome to the Online Quiz System!");
app.Run();