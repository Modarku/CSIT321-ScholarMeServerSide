using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RestTest.Models;

namespace RestTest
{
    public class ScholarMeDbContext : DbContext
    {
        public ScholarMeDbContext(DbContextOptions<ScholarMeDbContext> options) : base(options) { }

        public DbSet<UserAccount> Users { get; set; }
        public DbSet<FlashcardSet> FlashcardSets { get; set; }  
        public DbSet<FlashcardSetFlashcard> FlashcardSetFlashcards { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<FlashcardChoice> FlashcardsChoice { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("ScholarMeDbConnectionString")
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount()
                {
                    UserId = 1,
                    Username = "Cher",
                    Email = "cher@gmail.com",
                    Password = PasswordHash.EnhancedHashPassword("cherpassword", 16),
                    FirstName = "Teach",
                    LastName = "Cher",
                    PhoneNumber = "01234567890",
                    ProfilePic = null,
                    Role = UserRole.Teacher,
                    Status = UserStatus.Active,
                },
                new UserAccount()
                {
                    UserId = 2,
                    Username = "Perpy",
                    Email = "perpy@gmail.com",
                    Password = PasswordHash.EnhancedHashPassword("perpypassword", 16),
                    FirstName = "Van",
                    LastName = "Perpetua",
                    PhoneNumber = "01234567890",
                    ProfilePic = null,
                    Role = UserRole.Student,
                    Status = UserStatus.Active,
                },
                new UserAccount()
                {
                    UserId = 3,
                    Username = "Junsayke",
                    Email = "junsayke@gmail.com",
                    Password = PasswordHash.EnhancedHashPassword("junsaykepassword", 16),
                    FirstName = "Tonio",
                    LastName = "Ubaldo",
                    PhoneNumber = "01234567890",
                    ProfilePic = null,
                    Role = UserRole.Student,
                    Status = UserStatus.Active,
                },
                new UserAccount()
                {
                    UserId = 4,
                    Username = "Modarku",
                    Email = "modarku@gmail.com",
                    Password = PasswordHash.EnhancedHashPassword("modarkupassword", 16),
                    FirstName = "Jian",
                    LastName = "Olamit",
                    PhoneNumber = "01234567890",
                    ProfilePic = null,
                    Role = UserRole.Student,
                    Status = UserStatus.Inactive,
                }
            );
        }
    }
}
