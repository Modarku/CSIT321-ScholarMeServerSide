using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.FlashcardDeck
{
    public class FlashcardDeckUpdateDto
    {
        [MinLength(1)]
        [MaxLength(100)]
        public string? Title { get; set; }

        [MinLength(1)]
        [MaxLength(1000)]
        public string? Description { get; set; }
    }
}
