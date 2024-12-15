using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.DTO.UserAccount;
using System.Text.Json.Serialization;

namespace ScholarMeServer.DTO.FlashcardDeck
{
    // Primarily for displaying flashcard sets
    public class FlashcardDeckReadOnlyDto
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
