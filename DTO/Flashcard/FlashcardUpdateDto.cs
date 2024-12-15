using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.Flashcard
{
    public class FlashcardUpdateDto
    {
        public Guid? FlashcardSetId { get; set; }

        [MinLength(1)]
        [MaxLength(255)]
        public string? Question { get; set; }
    }
}
