using ScholarMeServer.DTO.FlashcardChoice;
using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.Flashcard
{
    // Primarily for creating flashcards
    public class FlashcardCreateDto
    {
        [Required]
        public int FlashcardSetId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Question { get; set; }
    }
}
