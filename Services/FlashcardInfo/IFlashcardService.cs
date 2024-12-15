using ScholarMeServer.DTO.Flashcard;

namespace ScholarMeServer.Services.FlashcardInfo
{
    public interface IFlashcardService
    {
        public Task<FlashcardReadOnlyDto> CreateFlashcard(int flashcardDeckId, FlashcardCreateDto flashcardDto);
        public Task<List<FlashcardReadOnlyDto>> GetFlashcardsByDeckId(int flashcardDeckId, bool choices);
        public Task<FlashcardReadOnlyDto> GetFlashcardById(int flashcardId);
        public Task<FlashcardReadOnlyDto> UpdateFlashcard(int flashcardId, FlashcardUpdateDto flashcardDto);
        public Task DeleteFlashcard(int flashcardId);
    }
}
