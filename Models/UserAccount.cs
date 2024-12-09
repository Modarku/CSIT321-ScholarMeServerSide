using System.ComponentModel.DataAnnotations;

// Model for individual user accounts
namespace RestTest.Models
{
    public class UserAccount
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public List<FlashcardDeck> FlashcardSets { get; set; }
    }
}
