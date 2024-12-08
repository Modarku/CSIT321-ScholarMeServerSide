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

        public async Task<List<FlashcardSet>> GetFlashcardSets(int userAccountId)
        {
            var flashcardSets = await _scholarmeDbContext.FlashcardSets.Where(flashcardSet => flashcardSet.UserAccountId == userAccountId)
                .Include(flashcardSet => flashcardSet.Flashcards)
                .ToListAsync();
            return flashcardSets;
        }
        public async Task<FlashcardSet?> GetFlashcardSetById(int id)
        {
            var flashcardSet = await _scholarmeDbContext.FlashcardSets.FindAsync(id);
            return flashcardSet;
        }
        public async Task<FlashcardSet> CreateFlashcardSet(FlashcardSet flashcardSet)
        {
            _scholarmeDbContext.FlashcardSets.Add(flashcardSet);
            await _scholarmeDbContext.SaveChangesAsync();

            return flashcardSet;
        }
        public async Task<FlashcardSet?> UpdateFlashcardSet(int id, FlashcardSet flashcardSet)
        {
            var existingFlashcardSet = await _scholarmeDbContext.FlashcardSets.FindAsync(id);

            if (existingFlashcardSet != null)
            {
                existingFlashcardSet.Title = flashcardSet.Title;
                existingFlashcardSet.Description = flashcardSet.Description;
                existingFlashcardSet.UpdatedAt = DateTime.Now;
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
