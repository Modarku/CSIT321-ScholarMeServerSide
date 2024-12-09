using Microsoft.EntityFrameworkCore;
using RestTest.Models;

namespace RestTest
{
    public class ScholarMeDbContext : DbContext
    {
        public ScholarMeDbContext(DbContextOptions<ScholarMeDbContext> options) : base(options) { }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<FlashcardChoice> FlashcardChoices { get; set; }
        public DbSet<FlashcardDeck> FlashcardDecks { get; set; }

        //public DbSet<FlashcardSetFlashcard> FlashcardSetFlashcards { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseSqlServer("ScholarMeDbConnectionString")
        //        .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Setup table columns

            modelBuilder.Entity<UserAccount>().HasData(
                    new UserAccount()
                    {
                        Id = 1,
                        Username = "Cher",
                        Email = "cher@gmail.com",
                        Password = HashPassword("hashme"),
                        FirstName = "Teach",
                        LastName = "Cher",
                    }
                );

            modelBuilder.Entity<FlashcardDeck>().HasData(
                    new FlashcardDeck()
                    {
                        Id = 1,
                        UserAccountId = 1,
                        Title = "Flashcard Set 1",
                        Description = "This is the first flashcard set",
                    }
                );

            modelBuilder.Entity<Flashcard>().HasData(
                    new Flashcard()
                    {
                        Id = 1,
                        FlashcardDeckId = 1,
                        Question = "What is the capital of France?",
                    }
                );

            modelBuilder.Entity<FlashcardChoice>().HasData(
                    new FlashcardChoice()
                    {
                        Id = 1,
                        FlashcardId = 1,
                        Choice = "Paris",
                        IsAnswer = true,
                    },
                    new FlashcardChoice()
                    {
                        Id = 2,
                        FlashcardId = 1,
                        Choice = "London",
                        IsAnswer = false,
                    },
                    new FlashcardChoice()
                    {
                        Id = 3,
                        FlashcardId = 1,
                        Choice = "Berlin",
                        IsAnswer = false,
                    }
                );
        }
    }
}
