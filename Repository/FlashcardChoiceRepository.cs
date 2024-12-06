using RestTest.Models;
using RestTest.Repository.IRepository;
using RestTest.Services.IServices;

namespace RestTest.Repository
{
    public class FlashcardChoiceRepository : IFlashcardChoiceRepository
    {
        private readonly IFlashcardService _service;
        public FlashcardChoiceRepository(IFlashcardService service)
        {
            _service = service;
        }
        public List<FlashcardChoice> GetAllFlashcardChoices()
        {
            return DB.FlashcardChoices;
        }
        public FlashcardChoice? GetFlashcardChoiceById(int id)
        {
            return DB.FlashcardChoices.SingleOrDefault(x => x.FlashcardChoiceId == id);
        }

        public List<FlashcardChoice>? GetAllFlashcardChoicesByFlashcardID(int fcid)
        {
            return DB.FlashcardChoices.Where(x => x.FlashcardId == fcid).ToList() ?? new List<FlashcardChoice>();
        }
    }
}
