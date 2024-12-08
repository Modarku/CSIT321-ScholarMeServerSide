using ScholarMeServer.DTO.FlashcardChoice;

namespace ScholarMeServer.DTO.Flashcard
{
    // Primarily for creating flashcards
    public class FlashcardCreateDto
    {
        public int FlashcardSetId { get; set; }
        public string Question { get; set; }
    }
}
