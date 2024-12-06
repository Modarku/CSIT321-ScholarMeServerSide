using RestTest.Models;

namespace RestTest.Repository.IRepository
{
    public interface IFlashcardSetRepository
    {
        public List<FlashcardSet> GetAllFlashcardSets();
        public List<FlashcardSet>? GetAllFlashcardSetsByUserId(int uid);
        public FlashcardSet? GetFlashcardSetById(int fcsid, int uid);
    }
}
