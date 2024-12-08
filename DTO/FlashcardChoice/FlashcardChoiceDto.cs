namespace ScholarMeServer.DTO.FlashcardChoice
{
    public class FlashcardChoiceDto
    {
        public int Id { get; set; }

        public int FlashcardId { get; set; }

        public string Choice { get; set; }

        public bool IsAnswer { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
