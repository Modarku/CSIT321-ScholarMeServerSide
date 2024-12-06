using RestTest.Models;

namespace RestTest.Repository.IRepository
{
    public interface IFlashcardRepository
    {
        public List<Flashcard> GetAllFlashcards();
        public List<Flashcard> GetAllFlashcardsBySetId(int fcsid);
        public Flashcard? GetFlashcardById(int fcsid, int fcid);
    }
}
