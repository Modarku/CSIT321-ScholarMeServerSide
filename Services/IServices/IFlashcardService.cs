using RestTest.DTO;
using RestTest.Models;

namespace RestTest.Services.IServices
{
    public interface IFlashcardService
    {
        public List<FlashcardDTO> GetAllFlashcards();
        public List<FlashcardDTO> GetAllFlashcardsBySetId(int fcsid);
        public FlashcardDTO? GetFlashcardById(int fcsid, int fcid);
    }
}
