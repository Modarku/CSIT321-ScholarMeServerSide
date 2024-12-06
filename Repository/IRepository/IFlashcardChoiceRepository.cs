using RestTest.Models;

namespace RestTest.Repository.IRepository
{
    public interface IFlashcardChoiceRepository
    {
        public List<FlashcardChoice> GetAllFlashcardChoices();
        public FlashcardChoice? GetFlashcardChoiceById(int id);
        public List<FlashcardChoice>? GetAllFlashcardChoicesByFlashcardID(int fcid);
    }
}
