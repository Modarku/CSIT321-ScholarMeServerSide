using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardInfo
{
    public interface IFlashcardInfoRepository
    {
        public Task<IEnumerable<Flashcard>> GetFlashcardsAsync();
        public Task<Flashcard?> GetFlashcardByIdAsync(int id);
        public Task<Flashcard> CreateFlashcardAsync(Flashcard flashcard);
        public Task<Flashcard?> UpdateFlashcardAsync(int id, Flashcard flashcard);
        public Task DeleteFlashcardAsync(int id);
    }
}
