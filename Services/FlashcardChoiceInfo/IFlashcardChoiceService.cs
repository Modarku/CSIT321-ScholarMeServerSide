using ScholarMeServer.DTO.FlashcardChoice;

namespace ScholarMeServer.Services.FlashcardChoiceInfo
{
    public interface IFlashcardChoiceService
    {
        public Task<FlashcardChoiceReadOnlyDto> CreateFlashcardChoice(Guid flashcardId, FlashcardChoiceCreateDto flashcardChoiceDto);

        public Task<List<FlashcardChoiceReadOnlyDto>> GetFlashcardChoicesByCardId(Guid flashcardId);

        public Task<FlashcardChoiceReadOnlyDto> GetFlashcardChoiceById(Guid flashcardChoiceId);

        public Task<FlashcardChoiceReadOnlyDto> UpdateFlashcardChoice(Guid flashcardChoiceId, FlashcardChoiceUpdateDto flashcardChoiceDto);
        public Task DeleteFlashcardChoice(Guid flashcardChoiceId);
    }
}
