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

            UserAccount user1 = new UserAccount()
            {
                Id = Guid.NewGuid(),
                Username = "Cher".ToLower(),
                Email = "Cher@gmail.com".ToLower(),
                Password = HashPassword("hashme"),
                FirstName = "Teach",
                LastName = "Cher",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            FlashcardDeck deck1 = new FlashcardDeck()
            {
                Id = Guid.NewGuid(),
                UserAccountId = user1.Id,
                Title = "Flashcard Set 1",
                Description = "This is the first flashcard set",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            Flashcard card1 = new Flashcard()
            {
                Id = Guid.NewGuid(),
                FlashcardDeckId = deck1.Id,
                Question = "What is the capital of France?",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            FlashcardChoice card1Choice1 = new FlashcardChoice()
            {
                Id = Guid.NewGuid(),
                FlashcardId = card1.Id,
                Choice = "Paris",
                IsAnswer = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            FlashcardChoice card1Choice2 = new FlashcardChoice()
            {
                Id = Guid.NewGuid(),
                FlashcardId = card1.Id,
                Choice = "London",
                IsAnswer = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            FlashcardChoice card1Choice3 = new FlashcardChoice()
            {
                Id = Guid.NewGuid(),
                FlashcardId = card1.Id,
                Choice = "Berlin",
                IsAnswer = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };


            modelBuilder.Entity<UserAccount>().HasData(user1);

            modelBuilder.Entity<FlashcardDeck>().HasData(deck1);

            modelBuilder.Entity<Flashcard>().HasData(card1);

            modelBuilder.Entity<FlashcardChoice>().HasData(card1Choice1, card1Choice2, card1Choice3);
        }
    }
}
