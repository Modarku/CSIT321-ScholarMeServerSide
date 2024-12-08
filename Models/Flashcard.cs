using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

// Model for independent flashcards
namespace RestTest.Models
{
    public class Flashcard
    {
        public int Id { get; set; }

        // Many-to-One relationship with FlashcardSet
        public int FlashcardSetId { get; set; }

        public string Question { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public List<FlashcardChoice> Choices { get; set; }

        public FlashcardSet FlashcardSet { get; set; }
    }
}
