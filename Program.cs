using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestTest;
using ScholarMeServer.Repository.FlashcardSetInfo;
using ScholarMeServer.Repository.UserAccountInfo;
using ScholarMeServer.Services.FlashcardSetInfo;
using ScholarMeServer.Services.UserAccountInfo;
using ScholarMeServer.Utilities;
using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication Setup
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Add services to the container.

// Inject Jwt utility class as singleton
builder.Services.AddSingleton<Jwt>();

// OLD Services
//builder.Services.AddTransient<IUserAccountService, UserAccountService>();
//builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
//builder.Services.AddTransient<IFlashcardSetService, FlashcardSetService>();
//builder.Services.AddScoped<IFlashcardSetRepository, FlashcardSetRepository>();
//builder.Services.AddTransient<IFlashcardService, FlashcardService>();
//builder.Services.AddScoped<IFlashcardRepository, FlashcardRepository>();
//builder.Services.AddTransient<IFlashcardSetFlashcardService, FlashcardSetFlashcardService>();
//builder.Services.AddScoped<IFlashcardSetFlashcardRepository, FlashcardSetFlashcardRepository>();
//builder.Services.AddTransient<IFlashcardChoiceService, FlashcardChoiceService>();
//builder.Services.AddScoped<IFlashcardChoiceRepository, FlashcardChoiceRepository>();

builder.Services.AddTransient<IUserAccountInfoService, UserAccountInfoService>();
builder.Services.AddScoped<IUserAccountInfoRepository, UserAccountInfoRepository>();

builder.Services.AddTransient<IFlashcardSetInfoService, FlashcardSetInfoService>();
builder.Services.AddScoped<IFlashcardSetInfoRepository, FlashcardSetInfoRepository>();

//builder.Services.AddTransient<IFlashcardInfoService, FlashcardInfoService>();
//builder.Services.AddScoped<IFlashcardInfoRepository, FlashcardInfoRepository>();
    
builder.Services.AddDbContext<ScholarMeDbContext>(
    db => db.UseNpgsql(builder.Configuration.GetConnectionString("ScholarMeDbConnectionString")), 
    ServiceLifetime.Scoped
);


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
