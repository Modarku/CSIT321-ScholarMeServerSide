using ScholarMeServer.DTO.FlashcardChoice;

namespace ScholarMeServer.DTO.Flashcard
{
    // Primarily for creating flashcards
    public class FlashcardNewDto
    {
        public int FlashcardSetId { get; set; }
        public string Question { get; set; }
    }
}
