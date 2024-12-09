using ScholarMeServer.DTO.FlashcardSet;

namespace ScholarMeServer.Services.FlashcardSetInfo
{
    public interface IFlashcardSetInfoService
    {
        public Task<List<FlashcardDeckReadOnlyDto>> GetFlashcardSets(int userAccountId);
        public Task<FlashcardDeckReadOnlyDto?> GetFlashcardSetById(int id);
        public Task<FlashcardDeckReadOnlyDto> CreateFlashcardSet(FlashcardDeckNewDto flashcardSetDto);
        public Task<FlashcardDeckReadOnlyDto?> UpdateFlashcardSet(int id, FlashcardDeckUpdateDto flashcardSetDto);
        public Task DeleteFlashcardSet(int id);
    }
}
