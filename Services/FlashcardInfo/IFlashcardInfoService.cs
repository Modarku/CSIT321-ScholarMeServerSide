using RestTest.Models;
using ScholarMeServer.DTO.Flashcard;

namespace ScholarMeServer.Services.FlashcardInfo
{
    public interface IFlashcardInfoService
    {
        public Task<List<FlashcardReadOnlyDto>> GetFlashcards(int flashcardSetId);
        public Task<FlashcardReadOnlyDto?> GetFlashcardById(int id);
        public Task<FlashcardReadOnlyDto> CreateFlashcard(FlashcardNewDto flashcardDto);
        public Task<FlashcardReadOnlyDto?> UpdateFlashcard(int id, FlashcardNewDto flashcardDto);
        public Task DeleteFlashcard(int id);
    }
}
