using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;
using ScholarMeServer.DTO.FlashcardSet;

namespace ScholarMeServer.Repository.FlashcardSetInfo
{
    public class FlashcardSetInfoRepository : IFlashcardSetInfoRepository
    {
        private readonly ScholarMeDbContext _scholarmeDbContext;

        public FlashcardSetInfoRepository(ScholarMeDbContext scholarmeDbContext)
        {
            _scholarmeDbContext = scholarmeDbContext;
        }

        public async Task<List<FlashcardDeck>> GetFlashcardSets(int userAccountId)
        {
            var flashcardSets = await _scholarmeDbContext.FlashcardSets.Where(flashcardSet => flashcardSet.UserAccountId == userAccountId)
                .Include(flashcardSet => flashcardSet.Flashcards)
                .ToListAsync();
            return flashcardSets;
        }
        public async Task<FlashcardDeck?> GetFlashcardSetById(int id)
        {
            var flashcardSet = await _scholarmeDbContext.FlashcardSets.FindAsync(id);
            return flashcardSet;
        }
        public async Task<FlashcardDeck> CreateFlashcardSet(FlashcardDeck flashcardSet)
        {
            _scholarmeDbContext.FlashcardSets.Add(flashcardSet);
            await _scholarmeDbContext.SaveChangesAsync();

            return flashcardSet;
        }
        public async Task<FlashcardDeck?> UpdateFlashcardSet(int id, FlashcardDeck flashcardSet)
        {
            var existingFlashcardSet = await _scholarmeDbContext.FlashcardSets.FindAsync(id);

            if (existingFlashcardSet != null)
            {
                existingFlashcardSet.Title = flashcardSet.Title;
                existingFlashcardSet.Description = flashcardSet.Description;
                existingFlashcardSet.UpdatedAt = DateTime.UtcNow;
                await _scholarmeDbContext.SaveChangesAsync();
            }
            return existingFlashcardSet;
        }
        public async Task DeleteFlashcardSet(int id)
        {
            var flashcardSet = await _scholarmeDbContext.FlashcardSets.FindAsync(id);
            if (flashcardSet != null)
            {
                _scholarmeDbContext.FlashcardSets.Remove(flashcardSet);
                await _scholarmeDbContext.SaveChangesAsync();
            }
        }

        public bool UserAccountExists(int userAccountId)
        {
            return _scholarmeDbContext.Users.Any(userAccount => userAccount.Id == userAccountId);
        }
    }
}
