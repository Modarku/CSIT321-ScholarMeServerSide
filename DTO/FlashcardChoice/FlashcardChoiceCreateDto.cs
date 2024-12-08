namespace ScholarMeServer.DTO.FlashcardChoice
{
    public class FlashcardChoiceCreateDto
    {
        public int FlashcardId { get; set; }

        public string Choice { get; set; }

        public bool IsAnswer { get; set; } = false;
    }
}
