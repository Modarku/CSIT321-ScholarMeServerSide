using ScholarMeServer.DTO.FlashcardChoice;
using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.Flashcard
{
    // Primarily for creating flashcards
    public class FlashcardCreateDto
    {
        // Comment out since flashcardDeckId is already provided in the api url {flashcardDeckId}
        //[Required]
        //[Range(1, int.MaxValue)]
        //public int FlashcardDeckId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Question { get; set; }
    }
}
