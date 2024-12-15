using ScholarMeServer.DTO.FlashcardChoice;
using System.Text.Json.Serialization;

namespace ScholarMeServer.DTO.Flashcard
{
    // Primarily for displaying flashcards
    public class FlashcardReadOnlyDto
    {
        public int Id { get; set; }

        public int FlashcardSetId { get; set; }

        public string Question { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FlashcardChoiceReadOnlyDto>? Choices { get; set; }
    }
}
