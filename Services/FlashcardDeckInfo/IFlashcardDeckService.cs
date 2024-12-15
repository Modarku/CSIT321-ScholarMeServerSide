using ScholarMeServer.DTO.FlashcardDeck;

namespace ScholarMeServer.Services.FlashcardDeckInfo
{
    public interface IFlashcardDeckService
    {
        public Task<FlashcardDeckReadOnlyDto> CreateFlashcardDeck(Guid userAccountId, FlashcardDeckCreateDto flashcardDeckDto);
        public Task<List<FlashcardDeckReadOnlyDto>> GetFlashcardDecksByUserId(Guid userAccountId);

        public Task<FlashcardDeckReadOnlyDto> GetFlashcardDeckById(Guid flashcardDeckId, bool includeFlashcards);
        public Task<FlashcardDeckReadOnlyDto> UpdateFlashcardDeck(Guid flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto);
        public Task DeleteFlashcardDeck(Guid flashcardDeckId);
    }
}
