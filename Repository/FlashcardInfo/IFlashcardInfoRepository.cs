using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardInfo
{
    public interface IFlashcardInfoRepository
    {
        public Task<List<Flashcard>> GetFlashcards(int flashcardSetId);
        public Task<Flashcard?> GetFlashcardById(int id);
        public Task<Flashcard> CreateFlashcard(Flashcard flashcard);
        public Task<Flashcard?> UpdateFlashcard(int id, Flashcard flashcard);
        public Task DeleteFlashcard(int id);
        public bool FlashcardSetExists(int flashcardSetId);
    }
}
