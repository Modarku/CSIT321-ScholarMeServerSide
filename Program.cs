using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestTest;
using ScholarMeServer.Repository.FlashcardChoiceInfo;
using ScholarMeServer.Repository.FlashcardDeckInfo;
using ScholarMeServer.Repository.FlashcardInfo;
using ScholarMeServer.Repository.UserAccountInfo;
using ScholarMeServer.Services.FlashcardChoiceInfo;
using ScholarMeServer.Services.FlashcardDeckInfo;
using ScholarMeServer.Services.FlashcardInfo;
using ScholarMeServer.Services.UserAccountInfo;
using ScholarMeServer.Utilities.Filters;
using ScholarMeServer.Utilities.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Consider using AutoMapper for automatic conversion from DTO to Model and vice versa

// Configure CORS - to allow requests from client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:8081") // Add your client URL here
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// JWT Authentication Setup
// https://medium.com/@solomongetachew112/jwt-authentication-in-net-8-a-complete-guide-for-secure-and-scalable-applications-6281e5e8667c
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
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
builder.Services.AddSingleton<JwtService>();

builder.Services.AddTransient<IUserAccountInfoService, UserAccountInfoService>();
builder.Services.AddScoped<IUserAccountInfoRepository, UserAccountInfoRepository>();

builder.Services.AddTransient<IFlashcardDeckService, FlashcardDeckService>();
builder.Services.AddScoped<IFlashcardDeckRepository, FlashcardDeckRepository>();

builder.Services.AddTransient<IFlashcardService, FlashcardService>();
builder.Services.AddScoped<IFlashcardRepository, FlashcardRepository>();

builder.Services.AddTransient<IFlashcardChoiceService, FlashcardChoiceService>();
builder.Services.AddScoped<IFlashcardChoiceRepository,  FlashcardChoiceRepository>();

builder.Services.AddDbContext<ScholarMeDbContext>(
    db => db.UseNpgsql(builder.Configuration.GetConnectionString("ScholarMeDbConnectionString")), 
    ServiceLifetime.Scoped
);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelValidatorFilter>();
});


builder.Services.AddControllers();

// Disable ModelState validation filter in order to be able to catch model validation exceptions in the controller
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Comment out when using the addition of custom middleware that automatically read JWT from cookies.
// https://www.youtube.com/watch?v=w8I32UPEvj8&t=133s
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Enter your JWT Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<String>()
        }
    });
});

// Register the custom exception handler middleware
builder.Services.AddTransient<ExceptionHandlerMiddleware>();

// Configure Kestrel to use HTTP
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Listen on port 5000 for HTTP
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Temporarily comment out in development, not recommended for production (altho need to setup SSL)
//app.UseHttpsRedirection();

// Apply CORS middleware
app.UseCors("AllowSpecificOrigin");

//// Custom middleware to read JWT token from cookies
//app.Use(async (context, next) =>
//{
//    var token = context.Request.Cookies["token"];
//    if (!string.IsNullOrEmpty(token))
//    {
//        context.Request.Headers.Append("Authorization", "Bearer " + token);
//    }
//    await next();
//});

// Use the custom exception handler middleware
// Handle Global Exception in the case it is not caught with Action Filters
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
