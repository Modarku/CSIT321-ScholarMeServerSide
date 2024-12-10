using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.FlashcardChoice
{
    public class FlashcardChoiceUpdateDto
    {
        [MinLength(1)]
        [MaxLength(255)]
        public string? Choice { get; set; }

        public bool? IsAnswer { get; set; } = false;
    }
}
