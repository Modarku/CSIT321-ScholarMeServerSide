using RestTest.Models;
using ScholarMeServer.DTO.Flashcard;

namespace ScholarMeServer.Services.FlashcardInfo
{
    public interface IFlashcardInfoService
    {
        public Task<IEnumerable<FlashcardDto>> GetFlashcardsAsync();
        public Task<FlashcardDto?> GetFlashcardByIdAsync(int id);
        public Task<FlashcardDto> CreateFlashcardAsync(FlashcardCreateDto flashcardDto);
        public Task<FlashcardDto?> UpdateFlashcardAsync(int id, FlashcardCreateDto flashcardDto);
        public Task DeleteFlashcardAsync(int id);
    }
}
