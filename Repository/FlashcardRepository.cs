using RestTest.Models;
using RestTest.Repository.IRepository;
using RestTest.Services.IServices;

namespace RestTest.Repository
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly IFlashcardSetFlashcardService _service;
        public FlashcardRepository(IFlashcardSetFlashcardService service)
        {
            _service = service;
        }

        public List<Flashcard> GetAllFlashcards()
        {
            return DB.Flashcards;
        }

        public List<Flashcard> GetAllFlashcardsBySetId(int fcsid)
        {
            var flashcardSetFlashcard = _service.GetFlashcardSetFlashcardByFlashcardSet(fcsid);

            if (flashcardSetFlashcard == null)
                return new List<Flashcard>();

            return DB.Flashcards.Where(x => x.FlashcardId == flashcardSetFlashcard.First().FlashcardId).ToList() ?? new List<Flashcard>();
        }

        public Flashcard? GetFlashcardById(int fcsid, int fcid)
        {
            var flashcard = GetAllFlashcardsBySetId(fcsid);

            return flashcard.SingleOrDefault(x => x.FlashcardId == fcid);
        }
    }
}
