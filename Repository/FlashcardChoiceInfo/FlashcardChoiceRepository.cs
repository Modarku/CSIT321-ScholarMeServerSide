using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardChoiceInfo
{
    public class FlashcardChoiceRepository : BaseRepository, IFlashcardChoiceRepository
    {
        public FlashcardChoiceRepository(ScholarMeDbContext scholarMeDbContext) : base(scholarMeDbContext) {}

        public async Task AddFlashcardChoice(FlashcardChoice flashcardChoice)
        {
            _scholarmeDbContext.Set<FlashcardChoice>().Add(flashcardChoice);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task<List<FlashcardChoice>> GetFlashcardChoicesByCardId(int flashcardId)
        {
            var flashcardChoices = await _scholarmeDbContext.Set<FlashcardChoice>().Where(fc => fc.FlashcardId == flashcardId).ToListAsync();
            return flashcardChoices;
        }

        public async Task<FlashcardChoice?> GetFlashcardChoiceById(int flashcardChoiceId)
        {
            var flashcard = await _scholarmeDbContext.Set<FlashcardChoice>().FindAsync(flashcardChoiceId);
            return flashcard;
        }

        public async Task SaveFlashcardChoice(FlashcardChoice flashcardChoice)
        {
            _scholarmeDbContext.Set<FlashcardChoice>().Update(flashcardChoice);
            await _scholarmeDbContext.SaveChangesAsync();
        }
        public async Task DeleteFlashcardChoice(FlashcardChoice flashcardChoice)
        {
            _scholarmeDbContext.Set<FlashcardChoice>().Remove(flashcardChoice);
            await _scholarmeDbContext.SaveChangesAsync();
        }
    }
}
