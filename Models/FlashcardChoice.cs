using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

// Model for individual flashcard choices
namespace RestTest.Models
{
    public class FlashcardChoice
    {
        public int Id { get; set; }

        // Many-to-One relationship with Flashcard
        public int FlashcardId { get; set; }

        public string Choice { get; set; }

        public bool IsAnswer { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public Flashcard Flashcard { get; set; }
    }
}
