using ScholarMeServer.DTO.FlashcardChoice;

namespace ScholarMeServer.Services.FlashcardChoiceInfo
{
    public interface IFlashcardChoiceService
    {
        public Task<FlashcardChoiceReadOnlyDto> CreateFlashcardChoice(int flashcardId, FlashcardChoiceCreateDto flashcardChoiceDto);

        public Task<List<FlashcardChoiceReadOnlyDto>> GetFlashcardChoicesByCardId(int flashcardId);

        public Task<FlashcardChoiceReadOnlyDto> GetFlashcardChoiceById(int flashcardChoiceId);

        public Task<FlashcardChoiceReadOnlyDto> UpdateFlashcardChoice(int flashcardChoiceId, FlashcardChoiceUpdateDto flashcardChoiceDto);
        public Task DeleteFlashcardChoice(int flashcardChoiceId);
    }
}
