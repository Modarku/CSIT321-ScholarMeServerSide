using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardDeckInfo
{
    public class FlashcardDeckRepository : IFlashcardDeckRepository
    {
        private readonly ScholarMeDbContext _scholarmeDbContext;

        public FlashcardDeckRepository(ScholarMeDbContext scholarmeDbContext)
        {
            _scholarmeDbContext = scholarmeDbContext;
        }

        public async Task<FlashcardDeck> CreateFlashcardDeck(FlashcardDeck flashcardDeck)
        {
            _scholarmeDbContext.FlashcardDecks.Add(flashcardDeck);
            await _scholarmeDbContext.SaveChangesAsync();

            return flashcardDeck;
        }

        public async Task<List<FlashcardDeck>> GetFlashcardDecks(int userAccountId)
        {
            var flashcardDecks = await _scholarmeDbContext.FlashcardDecks.Where(deck => deck.UserAccountId == userAccountId).ToListAsync();
            return flashcardDecks;
        }

        public async Task<FlashcardDeck?> GetFlashcardDeckById(int flashcardDeckId)
        {
            var flashcardDeck = await _scholarmeDbContext.FlashcardDecks.FindAsync(flashcardDeckId);
            return flashcardDeck;
        }

        public async Task DeleteFlashcardDeck(int flashcardDeckId)
        {
            var flashcardDeck = await _scholarmeDbContext.FlashcardDecks.FindAsync(flashcardDeckId);
            if (flashcardDeck != null)
            {
                _scholarmeDbContext.FlashcardDecks.Remove(flashcardDeck);
                await _scholarmeDbContext.SaveChangesAsync();
            }
        }

        public async Task SaveFlashcardDeck(FlashcardDeck flashcardDeck)
        {
            _scholarmeDbContext.Set<FlashcardDeck>().Update(flashcardDeck);
            await _scholarmeDbContext.SaveChangesAsync();
        }
    }
}
