using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardInfo
{
    public class FlashcardInfoRepository : IFlashcardInfoRepository
    {
        private readonly ScholarMeDbContext _scholarmeDbContext;

        public FlashcardInfoRepository(ScholarMeDbContext scholarmeDbContext)
        {
            _scholarmeDbContext = scholarmeDbContext;
        }
        public async Task<List<Flashcard>> GetFlashcards(int flashcardSetId)
        {
            var flashcards = await _scholarmeDbContext.Flashcards.Where(flashcard => flashcard.FlashcardSetId == flashcardSetId)
                .Include(flashcard => flashcard.Choices)
                .ToListAsync();
            return flashcards;
        }

        public async Task<Flashcard?> GetFlashcardById(int id)
        {
            var flashcard = await _scholarmeDbContext.Flashcards.Include(f => f.Choices).SingleOrDefaultAsync(f => f.Id == id);
            return flashcard;
        }

        public async Task<Flashcard> CreateFlashcard(Flashcard flashcard)
        {
            _scholarmeDbContext.Flashcards.Add(flashcard);
            await _scholarmeDbContext.SaveChangesAsync();

            return flashcard;
        }

        public async Task<Flashcard?> UpdateFlashcard(int id, Flashcard flashcard)
        {
            var existingFlashcard = await _scholarmeDbContext.Flashcards.Include(f => f.Choices).SingleOrDefaultAsync(f => f.Id == id);

            if (existingFlashcard != null)
            {
                existingFlashcard.Question = flashcard.Question;
                existingFlashcard.UpdatedAt = DateTime.UtcNow;
                await _scholarmeDbContext.SaveChangesAsync();
            }

            return existingFlashcard;
        }

        public async Task DeleteFlashcard(int id)
        {
            var flashcard = await _scholarmeDbContext.Flashcards.FindAsync(id);
            if (flashcard != null)
            {
                _scholarmeDbContext.Flashcards.Remove(flashcard);
                await _scholarmeDbContext.SaveChangesAsync();
            }
        }

        public bool FlashcardSetExists(int flashcardSetId)
        {
            return _scholarmeDbContext.FlashcardSets.Any(f => f.Id == flashcardSetId);
        }
    }
}
