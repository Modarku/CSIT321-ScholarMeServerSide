using RestTest.Repository.IRepository;
using RestTest.Repository;
using RestTest.Services.IServices;
using RestTest.Services;
using RestTest;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddTransient<IFlashcardSetService, FlashcardSetService>();
builder.Services.AddScoped<IFlashcardSetRepository, FlashcardSetRepository>();
builder.Services.AddTransient<IFlashcardService, FlashcardService>();
builder.Services.AddScoped<IFlashcardRepository, FlashcardRepository>();
builder.Services.AddTransient<IFlashcardSetFlashcardService, FlashcardSetFlashcardService>();
builder.Services.AddScoped<IFlashcardSetFlashcardRepository, FlashcardSetFlashcardRepository>();
builder.Services.AddTransient<IFlashcardChoiceService, FlashcardChoiceService>();
builder.Services.AddScoped<IFlashcardChoiceRepository, FlashcardChoiceRepository>();

var connectionString = builder.Configuration.GetConnectionString("ScholarMeDbConnectionString");

builder.Services.AddDbContext<ScholarMeDbContext>(
    db => db.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)), ServiceLifetime.Scoped
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
