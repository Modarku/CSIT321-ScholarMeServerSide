using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardDeckInfo
{
    public interface IFlashcardDeckRepository
    {
        public Task AddFlashcardDeck(FlashcardDeck flashcardDeck);
        public Task<List<FlashcardDeck>> GetFlashcardDecksByUserId(int userAccountId);
        public Task<FlashcardDeck?> GetFlashcardDeckById(int flashcardDeckId);
        public Task SaveFlashcardDeck(FlashcardDeck flashcardDeck);
        public Task DeleteFlashcardDeck(FlashcardDeck flashcardDeck);

    }
}
    