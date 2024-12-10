using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.Flashcard
{
    public class FlashcardUpdateDto
    {
        [Range(1, int.MaxValue)]
        public int? FlashcardSetId { get; set; }

        [MinLength(1)]
        [MaxLength(255)]
        public string? Question { get; set; }
    }
}
