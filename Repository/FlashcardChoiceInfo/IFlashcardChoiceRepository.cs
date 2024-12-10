using RestTest.Models;
using ScholarMeServer.DTO.FlashcardChoice;

namespace ScholarMeServer.Repository.FlashcardChoiceInfo
{
    public interface IFlashcardChoiceRepository
    {
        public Task AddFlashcardChoice(FlashcardChoice flashcardChoice);

        public Task<List<FlashcardChoice>> GetFlashcardChoicesByCardId(int flashcardId);

        public Task<FlashcardChoice?> GetFlashcardChoiceById(int flashcardChoiceId);

        public Task SaveFlashcardChoice(FlashcardChoice flashcardChoice);
        public Task DeleteFlashcardChoice(FlashcardChoice flashcardChoiceId);
    }
}
