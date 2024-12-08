using System.Text.Json.Serialization;

namespace ScholarMeServer.DTO.FlashcardChoice
{
    public class FlashcardChoiceInnerDto
    {
        public int Id { get; set; }

        public string Choice { get; set; }

        public bool IsAnswer { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
