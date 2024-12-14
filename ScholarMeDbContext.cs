using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RestTest.Models;
using ScholarMeServer.Models;

namespace RestTest
{
    public class ScholarMeDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ScholarMeDbContext(DbContextOptions<ScholarMeDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<FlashcardChoice> FlashcardChoices { get; set; }
        public DbSet<FlashcardDeck> FlashcardDecks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // Used to supressed when PendingModelChangesWarning is thrown although not recommended unless the schema and model are already in sync
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("ScholarMeDbConnectionString");

            optionsBuilder
                .UseNpgsql(connectionString)
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasData(
                    new UserAccount()
                    {
                        Id = 1,
                        Username = "Cher".ToLower(),
                        Email = "Cher@gmail.com".ToLower(),
                        Password = HashPassword("hashme"),
                        FirstName = "Teach",
                        LastName = "Cher",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    }
                );

            modelBuilder.Entity<FlashcardDeck>().HasData(
                    new FlashcardDeck()
                    {
                        Id = 1,
                        UserAccountId = 1,
                        Title = "Flashcard Set 1",
                        Description = "This is the first flashcard set",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    }
                );

            modelBuilder.Entity<Flashcard>().HasData(
                    new Flashcard()
                    {
                        Id = 1,
                        FlashcardDeckId = 1,
                        Question = "What is the capital of France?",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    }
                );

            modelBuilder.Entity<FlashcardChoice>().HasData(
                    new FlashcardChoice()
                    {
                        Id = 1,
                        FlashcardId = 1,
                        Choice = "Paris",
                        IsAnswer = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new FlashcardChoice()
                    {
                        Id = 2,
                        FlashcardId = 1,
                        Choice = "London",
                        IsAnswer = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new FlashcardChoice()
                    {
                        Id = 3,
                        FlashcardId = 1,
                        Choice = "Berlin",
                        IsAnswer = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    }
                );
        }
    }
}
