using ScholarMeServer.DTO.FlashcardChoice;
using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.Flashcard
{
    // Primarily for creating flashcards
    public class FlashcardCreateDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int FlashcardSetId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Question { get; set; }
    }
}
