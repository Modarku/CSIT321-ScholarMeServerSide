using RestTest.DTO;
using RestTest.Models;

namespace RestTest.Services.IServices
{
    public interface IFlashcardSetFlashcardService
    {
        public List<FlashcardSetFlashcardDTO> GetAllFlashcardSetFlashcards();
        public List<FlashcardSetFlashcardDTO> GetFlashcardSetFlashcardByFlashcard(int fcid);
        public List<FlashcardSetFlashcardDTO> GetFlashcardSetFlashcardByFlashcardSet(int fcsid);
        public FlashcardSetFlashcardDTO? GetFlashcardSetFlashcardById(int id);
    }
}
