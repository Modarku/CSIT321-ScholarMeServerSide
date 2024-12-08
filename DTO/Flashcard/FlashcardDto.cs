using ScholarMeServer.DTO.FlashcardChoice;

namespace ScholarMeServer.DTO.Flashcard
{
    public class FlashcardDto
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<FlashcardChoiceInnerDto>? Choices { get; set; }
    }
}
