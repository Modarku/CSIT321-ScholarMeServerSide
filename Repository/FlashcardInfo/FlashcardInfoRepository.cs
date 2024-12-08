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
        public async Task<IEnumerable<Flashcard>> GetFlashcardsAsync()
        {
            return await _scholarmeDbContext.Flashcards.Include(f => f.Choices).ToListAsync();
        }

        public async Task<Flashcard?> GetFlashcardByIdAsync(int id)
        {
            return await _scholarmeDbContext.Flashcards.Include(f => f.Choices).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Flashcard> CreateFlashcardAsync(Flashcard flashcard)
        {
            await _scholarmeDbContext.Flashcards.AddAsync(flashcard);
            await _scholarmeDbContext.SaveChangesAsync();

            await _scholarmeDbContext.Entry(flashcard).ReloadAsync();
            return flashcard;
        }

        public async Task<Flashcard?> UpdateFlashcardAsync(int id, Flashcard flashcard)
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

        public async Task DeleteFlashcardAsync(int id)
        {
            var flashcard = await _scholarmeDbContext.Flashcards.FindAsync(id);
            if (flashcard != null)
            {
                _scholarmeDbContext.Flashcards.Remove(flashcard);
            }
        }
    }
}
