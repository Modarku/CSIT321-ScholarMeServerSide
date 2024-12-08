namespace ScholarMeServer.DTO.Flashcard
{
    // Primarily for displaying flashcards
    public class FlashcardInnerDto
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
