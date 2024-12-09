using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardInfo
{
    public interface IFlashcardRepository
    {
        public Task AddFlashcard(Flashcard flashcard);
        public Task<List<Flashcard>> GetFlashcardsByDeckId(int flashcardDeckId);
        public Task<Flashcard?> GetFlashcardById(int flashcardId);
        public Task DeleteFlashcard(int flashcardId);
        public Task SaveFlashcard(Flashcard flashcard);
        public bool HasChanges();
        public Task<bool> FlashcardDeckExists(int flashcardDeckId);
    }
}
