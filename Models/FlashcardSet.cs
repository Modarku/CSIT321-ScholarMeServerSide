using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

// Model for grouping flashcards as a set
namespace RestTest.Models
{
    public class FlashcardSet
    {
        public int Id { get; set; }

        public int UserAccountId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Property

        public List<Flashcard> Flashcards { get; set; }

        public UserAccount UserAccount { get; set; }
    }
}
