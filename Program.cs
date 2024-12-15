using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-9.0
// https://blog.maartenballiauw.be/post/2022/09/26/aspnet-core-rate-limiting-middleware.html
builder.Services.AddRateLimiter(options =>
{
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            await context.HttpContext.Response.WriteAsync(
                $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s). ");
        }
        else
        {
            await context.HttpContext.Response.WriteAsync(
                "Too many requests. Please try again later. ");
        }
    };
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 60, // 60 requests per minute
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }));
});

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
            //.WithExposedHeaders("www-authenticate")); // Expose the www-authenticate header
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ClockSkew = TimeSpan.Zero, // Set clock skew to zero to approximately jwt expiration
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "File Upload API", Version = "v1" });
});

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
        Description = "Enter your JWT Access RefreshToken",
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

// Ensure the rate limiting middleware is added to the request pipeline
//app.UseRateLimiter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); // Enable serving static files
// Serve files from the "Media" folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Media")),
    RequestPath = "/Media"
});

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
