namespace ScholarMeServer.DTO.FlashcardChoice
{
    public class FlashcardChoiceNewDto
    {
        public int FlashcardId { get; set; }

        public string Choice { get; set; }

        public bool IsAnswer { get; set; } = false;
    }
}
