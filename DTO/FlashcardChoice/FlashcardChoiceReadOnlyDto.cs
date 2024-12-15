using System.Text.Json.Serialization;

namespace ScholarMeServer.DTO.FlashcardChoice
{
    public class FlashcardChoiceReadOnlyDto
    {
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? FlashcardId { get; set; }

        public string Choice { get; set; }

        public bool IsAnswer { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
