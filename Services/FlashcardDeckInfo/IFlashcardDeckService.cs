using ScholarMeServer.DTO.FlashcardDeck;

namespace ScholarMeServer.Services.FlashcardDeckInfo
{
    public interface IFlashcardDeckService
    {
        public Task<FlashcardDeckReadOnlyDto> CreateFlashcardDeck(int userAccountId, FlashcardDeckCreateDto flashcardDeckDto);
        public Task<List<FlashcardDeckReadOnlyDto>> GetFlashcardDecksByUserId(int userAccountId);

        public Task<FlashcardDeckReadOnlyDto> GetFlashcardDeckById(int flashcardDeckId);
        public Task<FlashcardDeckReadOnlyDto> UpdateFlashcardDeck(int flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto);
        public Task DeleteFlashcardDeck(int flashcardDeckId);
    }
}
