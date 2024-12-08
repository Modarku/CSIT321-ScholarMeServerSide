using ScholarMeServer.DTO.FlashcardSet;

namespace ScholarMeServer.Services.FlashcardSetInfo
{
    public interface IFlashcardSetInfoService
    {
        public Task<List<FlashcardSetDto>> GetFlashcardSets(int userAccountId);
        public Task<FlashcardSetDto?> GetFlashcardSetById(int id);
        public Task<FlashcardSetDto> CreateFlashcardSet(FlashcardSetCreateDto flashcardSetDto);
        public Task<FlashcardSetDto?> UpdateFlashcardSet(int id, FlashcardSetUpdateDto flashcardSetDto);
        public Task DeleteFlashcardSet(int id);
    }
}
