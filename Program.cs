using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestTest;
using ScholarMeServer.Repository.FlashcardDeckInfo;
using ScholarMeServer.Repository.UserAccountInfo;
using ScholarMeServer.Services.FlashcardDeckInfo;
using ScholarMeServer.Services.UserAccountInfo;
using ScholarMeServer.Utilities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Consider using AutoMapper for automatic conversion from DTO to Model and vice versa

// JWT Authentication Setup
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

builder.Services.AddTransient<IFlashcardDeckService, FlashcardDeckService>();
builder.Services.AddScoped<IFlashcardDeckRepository, FlashcardDeckRepository>();


//builder.Services.AddTransient<IFlashcardSetInfoService, FlashcardSetInfoService>();
//builder.Services.AddScoped<IFlashcardSetInfoRepository, FlashcardSetInfoRepository>();

//builder.Services.AddTransient<IFlashcardInfoService, FlashcardInfoService>();
//builder.Services.AddScoped<IFlashcardInfoRepository, FlashcardInfoRepository>();
    
builder.Services.AddDbContext<ScholarMeDbContext>(
    db => db.UseNpgsql(builder.Configuration.GetConnectionString("ScholarMeDbConnectionString")), 
    ServiceLifetime.Scoped
);


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

// Register the ownership middleware
app.UseMiddleware<OwnershipMiddleware>();

app.MapControllers();

app.Run();
