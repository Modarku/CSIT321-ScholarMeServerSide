using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestTest.Models;

namespace RestTest.DTO
{
    public class FlashcardSetFlashcardDTO
    {
        [ForeignKey(nameof(FlashcardSetId))]
        public int FlashcardSetId { get; set; }
        public FlashcardSet FlashcardSet { get; set; }

        [ForeignKey(nameof(FlashcardId))]
        public int FlashcardId { get; set; }
        public Flashcard Flashcard { get; set; }


    }
}
