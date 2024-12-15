using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

// Model for independent flashcards
namespace RestTest.Models
{
    public class Flashcard
    {
        public Guid Id { get; set; }

        // Many-to-One relationship with FlashcardDeck
        public Guid FlashcardDeckId { get; set; }

        public string Question { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public List<FlashcardChoice> Choices { get; set; }

        public FlashcardDeck FlashcardDeck { get; set; }
    }
}
