using RestTest.Models;
using ScholarMeServer.DTO.FlashcardSet;

namespace ScholarMeServer.Repository.FlashcardSetInfo
{
    public interface IFlashcardSetInfoRepository
    {
        public Task<List<FlashcardSet>> GetFlashcardSets(int userAccountId);
        public Task<FlashcardSet?> GetFlashcardSetById(int id);
        public Task<FlashcardSet> CreateFlashcardSet(FlashcardSet flashcardSet);
        public Task<FlashcardSet?> UpdateFlashcardSet(int id, FlashcardSet flashcardSet);
        public Task DeleteFlashcardSet(int id);
        public bool UserAccountExists(int userAccountId);
    }
}
