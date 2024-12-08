using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RestTest.Models;

namespace RestTest
{
    public class ScholarMeDbContext : DbContext
    {
        public ScholarMeDbContext(DbContextOptions<ScholarMeDbContext> options) : base(options) { }

        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<FlashcardChoice> FlashcardChoices { get; set; }
        public DbSet<FlashcardSet> FlashcardSets { get; set; }

        //public DbSet<FlashcardSetFlashcard> FlashcardSetFlashcards { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseSqlServer("ScholarMeDbConnectionString")
        //        .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup table columns
            modelBuilder.Entity<UserAccount>(e =>
            {
                e.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                e.Property(u => u.UpdatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Flashcard>(e =>
            {
                e.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                e.Property(u => u.UpdatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<FlashcardChoice>(e =>
            {
                e.Property(u => u.IsAnswer)
                        .HasDefaultValue(false);

                e.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                e.Property(u => u.UpdatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<FlashcardSet>(e =>
            {
                e.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                e.Property(u => u.UpdatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Comment out seeding data because for unknown reason it cause PendingModelChangesWarning
            // Seeding Data
            //modelBuilder.Entity<UserAccount>().HasData(
            //        new UserAccount()
            //        {
            //            Id = 1,
            //            Username = "hunyo",
            //            Email = "hunyo@gmail.com",
            //            Password = HashPassword("sekwret"),
            //            FirstName = "Jun",
            //            LastName = "Sayke",
            //        }
            //    );

            //modelBuilder.Entity<FlashcardSet>().HasData(
            //        new FlashcardSet()
            //        {
            //            Id = 1,
            //            UserAccountId = 1,
            //            Title = "Intelligent System I",
            //            Description = "Final Exam Reviewer",
            //        }
            //    );

            //modelBuilder.Entity<Flashcard>().HasData(
            //        new Flashcard()
            //        {
            //            Id = 1,
            //            FlashcardSetId = 1,
            //            Question = "What is Neural Networks?",
            //        }
            //    );

            //modelBuilder.Entity<FlashcardChoice>().HasData(
            //        new FlashcardChoice()
            //        {
            //            Id = 1,
            //            FlashcardId = 1,
            //            Choice = "An wifi network",
            //        },
            //        new FlashcardChoice()
            //        {
            //            Id = 2,
            //            FlashcardId = 1,
            //            Choice = "A brain stem",
            //        },
            //        new FlashcardChoice()
            //        {
            //            Id = 3,
            //            FlashcardId = 1,
            //            Choice = "A machine learning model",
            //            IsAnswer = true,
            //        }
            //    );
        }
    }
}
