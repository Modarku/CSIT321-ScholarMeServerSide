using RestTest.Models;
using ScholarMeServer.DTO.FlashcardSet;

namespace ScholarMeServer.Repository.FlashcardSetInfo
{
    public interface IFlashcardSetInfoRepository
    {
        public Task<List<FlashcardDeck>> GetFlashcardSets(int userAccountId);
        public Task<FlashcardDeck?> GetFlashcardSetById(int id);
        public Task<FlashcardDeck> CreateFlashcardSet(FlashcardDeck flashcardSet);
        public Task<FlashcardDeck?> UpdateFlashcardSet(int id, FlashcardDeck flashcardSet);
        public Task DeleteFlashcardSet(int id);
        public bool UserAccountExists(int userAccountId);
    }
}
