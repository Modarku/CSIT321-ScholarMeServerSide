using RestTest.Models;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace RestTest
{
    public class DB
    {
        public static List<UserAccount> Users = new List<UserAccount>
        {
            new UserAccount()
            {
                UserId = 0,
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
                UserId = 1,
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
                UserId = 2,
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
                UserId = 3,
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
        };

        public static List<FlashcardSet> FlashcardSets = new List<FlashcardSet>
        {
            new FlashcardSet()
            {
                FlashcardSetId = 1,
                UserId = 1,
                UserAccount = Users[1],
                Title = "Lawyer Things",
                Description = "I wanna be the very best.",
            }
        };

        public static List<Flashcard> Flashcards = new List<Flashcard>
        {
            new Flashcard()
            {
                FlashcardId = 1,
                UserId = 1,
                UserAccount = Users[1],
                Question = "Which company/person bought InfoWars from an auction?"
            },
            new Flashcard()
            {
                FlashcardId = 2,
                UserId = 1,
                UserAccount = Users[1],
                Question = "What song did Drake file a lawsuit for 'defamation'"
            }
        };

        public static List<FlashcardSetFlashcard> FlashcardSetFlashcards = new List<FlashcardSetFlashcard>
        {
            new FlashcardSetFlashcard()
            {
                FlashcardSetFlashcardId = 1,
                FlashcardSetId = 1,
                FlashcardSet = FlashcardSets[0],
                FlashcardId = 1,
                Flashcard = Flashcards[0]
            },
            new FlashcardSetFlashcard()
            {
                FlashcardSetFlashcardId = 2,
                FlashcardSetId = 1,
                FlashcardSet = FlashcardSets[0],
                FlashcardId = 2,
                Flashcard = Flashcards[1]
            }
        };

        public static List<FlashcardChoice> FlashcardChoices = new List<FlashcardChoice>
        {
            new FlashcardChoice()
            {
                FlashcardChoiceId = 1,
                FlashcardId = 1,
                Flashcard = Flashcards[0],
                Choice = "Elon Musk",
                IsAnswer = false,
            },
            new FlashcardChoice()
            {
                FlashcardChoiceId = 2,
                FlashcardId = 1,
                Flashcard = Flashcards[0],
                Choice = "The Onion",
                IsAnswer = true,
            },
            new FlashcardChoice()
            {
                FlashcardChoiceId = 3,
                FlashcardId = 2,
                Flashcard = Flashcards[1],
                Choice = "Not Like Us",
                IsAnswer = true,
            },
            new FlashcardChoice()
            {
                FlashcardChoiceId = 4,
                FlashcardId = 2,
                Flashcard = Flashcards[1],
                Choice = "Story of Adidon",
                IsAnswer = false,
            }
        };
    }
}
