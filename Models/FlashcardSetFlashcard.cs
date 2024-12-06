using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestTest.Models
{
    public class FlashcardSetFlashcard
    {
        [Key]
        public int FlashcardSetFlashcardId { get; set; }

        [ForeignKey(nameof(FlashcardSetId))]
        public int FlashcardSetId { get; set; }
        public FlashcardSet FlashcardSet { get; set; }

        [ForeignKey(nameof(FlashcardId))]
        public int FlashcardId { get; set; }
        public Flashcard Flashcard { get; set; }
    }
}
