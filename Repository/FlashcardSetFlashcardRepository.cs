using RestTest.Models;
using RestTest.Repository.IRepository;

namespace RestTest.Repository
{
    public class FlashcardSetFlashcardRepository : IFlashcardSetFlashcardRepository
    {
        public List<FlashcardSetFlashcard> GetAllFlashcardSetFlashcards()
        {
            return DB.FlashcardSetFlashcards;
        }

        public List<FlashcardSetFlashcard> GetFlashcardSetFlashcardByFlashcard(int fcid)
        {
            return DB.FlashcardSetFlashcards.Where(x => x.FlashcardId == fcid).ToList() ?? new List<FlashcardSetFlashcard>();
        }

        public List<FlashcardSetFlashcard> GetFlashcardSetFlashcardByFlashcardSet(int fcsid)
        {
            return DB.FlashcardSetFlashcards.Where(x => x.FlashcardSetId == fcsid).ToList() ?? new List<FlashcardSetFlashcard>();
        }

        public FlashcardSetFlashcard? GetFlashcardSetFlashcardById(int id)
        {
            return DB.FlashcardSetFlashcards.SingleOrDefault(x => x.FlashcardSetFlashcardId == id);
        }
    }
}
