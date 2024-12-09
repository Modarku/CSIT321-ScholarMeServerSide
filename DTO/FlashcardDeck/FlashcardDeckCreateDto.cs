using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.FlashcardDeck
{
    public class FlashcardDeckCreateDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
