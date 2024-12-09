using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.Flashcard
{
    public class FlashcardUpdateDto
    {
        public int? FlashcardSetId { get; set; }

        [MinLength(1)]
        [MaxLength(255)]
        public string? Question { get; set; }
    }
}
