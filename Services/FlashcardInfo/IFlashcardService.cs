using ScholarMeServer.DTO.Flashcard;

namespace ScholarMeServer.Services.FlashcardInfo
{
    public interface IFlashcardService
    {
        public Task<FlashcardReadOnlyDto> CreateFlashcard(int flashcardDeckId, FlashcardCreateDto flashcardDto);
        public Task<List<FlashcardReadOnlyDto>> GetFlashcardsByDeckId(int flashcardDeckId);
        public Task<FlashcardReadOnlyDto> UpdateFlashcard(int flashcardDeckId, FlashcardUpdateDto flashcardDto);
        public Task DeleteFlashcard(int flashcardDeckId);
    }
}
