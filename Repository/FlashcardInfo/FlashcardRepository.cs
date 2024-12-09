using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;
using ScholarMeServer.DTO.Flashcard;

namespace ScholarMeServer.Repository.FlashcardInfo
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly ScholarMeDbContext _scholarmeDbContext;

        public FlashcardRepository(ScholarMeDbContext scholarmeDbContext)
        {
            _scholarmeDbContext = scholarmeDbContext;
        }

        public async Task AddFlashcard( Flashcard flashcard)
        {
            _scholarmeDbContext.Flashcards.Add(flashcard);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task<List<Flashcard>> GetFlashcardsByDeckId(int flashcardDeckId)
        {
            var flashcards = await _scholarmeDbContext.Flashcards.Where(f => f.FlashcardSetId == flashcardDeckId).ToListAsync();
            return flashcards;
        }

        public async Task<Flashcard?> GetFlashcardById(int flashcardId)
        {
            var flashcard = await _scholarmeDbContext.Flashcards.FindAsync(flashcardId);
            return flashcard;
        }

        public async Task DeleteFlashcard(int flashcardId)
        {
            var flashcard = await GetFlashcardById(flashcardId);

            if (flashcard != null)
            {
                _scholarmeDbContext.Flashcards.Remove(flashcard);
                await _scholarmeDbContext.SaveChangesAsync();
            }
        }

        public async Task SaveFlashcard(Flashcard flashcard)
        {
            _scholarmeDbContext.Flashcards.Update(flashcard);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return _scholarmeDbContext.ChangeTracker.HasChanges();
        }

        public async Task<bool> FlashcardDeckExists(int flashcardDeckId)
        {
            return await _scholarmeDbContext.FlashcardDecks.AnyAsync(f => f.Id == flashcardDeckId);
        }
    }
}
