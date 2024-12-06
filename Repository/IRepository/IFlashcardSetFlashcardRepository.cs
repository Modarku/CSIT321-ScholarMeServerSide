using RestTest.Models;

namespace RestTest.Repository.IRepository
{
    public interface IFlashcardSetFlashcardRepository
    {
        public List<FlashcardSetFlashcard> GetAllFlashcardSetFlashcards();
        public List<FlashcardSetFlashcard> GetFlashcardSetFlashcardByFlashcard(int fcid);
        public List<FlashcardSetFlashcard> GetFlashcardSetFlashcardByFlashcardSet(int fcsid);
        public FlashcardSetFlashcard? GetFlashcardSetFlashcardById(int id);

    }
}
