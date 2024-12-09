using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.FlashcardChoice
{
    public class FlashcardChoiceCreateDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int FlashcardId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Choice { get; set; }

        [Required]
        public bool IsAnswer { get; set; } = false;
    }
}
