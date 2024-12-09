using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardDeckInfo
{
    public interface IFlashcardDeckRepository
    {
        public Task<FlashcardDeck> CreateFlashcardDeck(FlashcardDeck flashcardDeck);
        public Task<List<FlashcardDeck>> GetFlashcardDecks(int userAccountId);
        public Task<FlashcardDeck?> GetFlashcardDeckById(int flashcardDeckId);
        public Task SaveFlashcardDeck(FlashcardDeck flashcardDeck);
        public Task DeleteFlashcardDeck(int flashcardDeckId);
    }
}
    