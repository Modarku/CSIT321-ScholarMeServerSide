using RestTest.Models;
using RestTest.Repository.IRepository;

namespace RestTest.Repository
{
    public class FlashcardSetRepository : IFlashcardSetRepository
    {
        public List<FlashcardSet> GetAllFlashcardSets()
        {
            return DB.FlashcardSets;
        }
        public List<FlashcardSet>? GetAllFlashcardSetsByUserId(int uid)
        {
            return DB.FlashcardSets.Where(x => x.UserId == uid).ToList() ?? new List<FlashcardSet>();
        }

        public FlashcardSet? GetFlashcardSetById(int fcsid, int uid)
        {
            List<FlashcardSet> flashcardSets = GetAllFlashcardSetsByUserId(uid);
            return flashcardSets.SingleOrDefault(x => x.FlashcardSetId == fcsid);
        }
    }
}
