using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardInfo
{
    public class FlashcardRepository : BaseRepository, IFlashcardRepository
    {
        public FlashcardRepository(ScholarMeDbContext scholarmeDbContext) : base(scholarmeDbContext) { }

        public async Task AddFlashcard(Flashcard flashcard)
        {
            _scholarmeDbContext.Set<Flashcard>().Add(flashcard);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task<List<Flashcard>> GetFlashcardsByDeckId(Guid flashcardDeckId, bool includeChoices = false)
        {
            IQueryable<Flashcard> query = _scholarmeDbContext.Set<Flashcard>().Where(f => f.FlashcardDeckId == flashcardDeckId);

            if (includeChoices)
            {
                query = query.Include(f => f.Choices);
            }

            var flashcards = await query.ToListAsync();
            return flashcards;
        }

        public async Task<Flashcard?> GetFlashcardById(Guid flashcardId, bool includeChoices = false)
        {
            IQueryable<Flashcard> query = _scholarmeDbContext.Set<Flashcard>().Where(f => f.Id == flashcardId);

            if (includeChoices)
            {
                query = query.Include(f => f.Choices);
            }

            var flashcard = await query.FirstOrDefaultAsync();
            return flashcard;
        }

        public async Task SaveFlashcard(Flashcard flashcard)
        {
            _scholarmeDbContext.Set<Flashcard>().Update(flashcard);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task DeleteFlashcard(Flashcard flashcard)
        {
            _scholarmeDbContext.Set<Flashcard>().Remove(flashcard);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task<bool> FlashcardDeckExists(Guid flashcardDeckId)
        {
            return await _scholarmeDbContext.Set<FlashcardDeck>().AnyAsync(f => f.Id == flashcardDeckId);
        }
    }
}
