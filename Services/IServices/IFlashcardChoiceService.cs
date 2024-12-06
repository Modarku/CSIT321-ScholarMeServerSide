using RestTest.DTO;

namespace RestTest.Services.IServices
{
    public interface IFlashcardChoiceService
    {
        public List<FlashcardChoiceDTO> GetAllFlashcardChoices();
        public FlashcardChoiceDTO? GetFlashcardChoiceById(int id);
        public List<FlashcardChoiceDTO>? GetAllFlashcardChoicesByFlashcardID(int fcid);
    }
}
