using Microsoft.EntityFrameworkCore;
using RestTest;
using ScholarMeServer.Repository.FlashcardInfo;
using ScholarMeServer.Repository.UserAccountInfo;
using ScholarMeServer.Services.FlashcardInfo;
using ScholarMeServer.Services.UserAccountInfo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddTransient<IFlashcardInfoService, FlashcardInfoService>();
builder.Services.AddScoped<IFlashcardInfoRepository, FlashcardInfoRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
