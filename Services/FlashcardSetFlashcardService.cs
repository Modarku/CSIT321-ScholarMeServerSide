using RestTest.DTO;
using RestTest.Repository.IRepository;
using RestTest.Services.IServices;

namespace RestTest.Services
{
    public class FlashcardSetFlashcardService : IFlashcardSetFlashcardService
    {
        private readonly IFlashcardSetFlashcardRepository _repository;
        public FlashcardSetFlashcardService(IFlashcardSetFlashcardRepository repository)
        {
            _repository = repository;
        }

        public List<FlashcardSetFlashcardDTO> GetAllFlashcardSetFlashcards()
        {
            List<FlashcardSetFlashcardDTO> flashcardSetFlashcards = new List<FlashcardSetFlashcardDTO>();
            var rFlashcardSetFlashcards = _repository.GetAllFlashcardSetFlashcards();
        
            foreach (var rFlashcardSetFlashcard in rFlashcardSetFlashcards)
            {
                flashcardSetFlashcards.Add(new FlashcardSetFlashcardDTO()
                {
                    FlashcardSetId = rFlashcardSetFlashcard.FlashcardSetId,
                    FlashcardSet = rFlashcardSetFlashcard.FlashcardSet,
                    FlashcardId = rFlashcardSetFlashcard.FlashcardId,
                    Flashcard = rFlashcardSetFlashcard.Flashcard
                });
            }

            return flashcardSetFlashcards;
        }

        public List<FlashcardSetFlashcardDTO> GetFlashcardSetFlashcardByFlashcard(int fcid)
        {
            List<FlashcardSetFlashcardDTO> flashcardSetFlashcards = new List<FlashcardSetFlashcardDTO>();
            var rFlashcardSetFlashcards = _repository.GetFlashcardSetFlashcardByFlashcard(fcid);

            foreach (var rFlashcardSetFlashcard in rFlashcardSetFlashcards)
            {
                flashcardSetFlashcards.Add(new FlashcardSetFlashcardDTO()
                {
                    FlashcardSetId = rFlashcardSetFlashcard.FlashcardSetId,
                    FlashcardSet = rFlashcardSetFlashcard.FlashcardSet,
                    FlashcardId = rFlashcardSetFlashcard.FlashcardId,
                    Flashcard = rFlashcardSetFlashcard.Flashcard
                });
            }

            return flashcardSetFlashcards;
        }
        public List<FlashcardSetFlashcardDTO> GetFlashcardSetFlashcardByFlashcardSet(int fcsid)
        {
            List<FlashcardSetFlashcardDTO> flashcardSetFlashcards = new List<FlashcardSetFlashcardDTO>();
            var rFlashcardSetFlashcards = _repository.GetFlashcardSetFlashcardByFlashcardSet(fcsid);

            foreach (var rFlashcardSetFlashcard in rFlashcardSetFlashcards)
            {
                flashcardSetFlashcards.Add(new FlashcardSetFlashcardDTO()
                {
                    FlashcardSetId = rFlashcardSetFlashcard.FlashcardSetId,
                    FlashcardSet = rFlashcardSetFlashcard.FlashcardSet,
                    FlashcardId = rFlashcardSetFlashcard.FlashcardId,
                    Flashcard = rFlashcardSetFlashcard.Flashcard
                });
            }

            return flashcardSetFlashcards;
        }

        public FlashcardSetFlashcardDTO? GetFlashcardSetFlashcardById(int id)
        {
            var rFlashcardSetFlashcard = _repository.GetFlashcardSetFlashcardById(id);

            if (rFlashcardSetFlashcard == null)
                return null;

            return new FlashcardSetFlashcardDTO()
            {
                FlashcardSetId = rFlashcardSetFlashcard.FlashcardSetId,
                FlashcardSet = rFlashcardSetFlashcard.FlashcardSet,
                FlashcardId = rFlashcardSetFlashcard.FlashcardId,
                Flashcard = rFlashcardSetFlashcard.Flashcard
            };
        }
    }
}
