using RestTest.DTO;
using RestTest.Repository.IRepository;
using RestTest.Services.IServices;

namespace RestTest.Services
{
    public class FlashcardChoiceService : IFlashcardChoiceService
    {
        private readonly IFlashcardChoiceRepository _repository;
        public FlashcardChoiceService(IFlashcardChoiceRepository repository)
        {
            _repository = repository;
        }

        public List<FlashcardChoiceDTO> GetAllFlashcardChoices()
        {
            List<FlashcardChoiceDTO> flashcardChoices = new List<FlashcardChoiceDTO>();
            var rFlashcardChoices = _repository.GetAllFlashcardChoices();

            foreach (var rFlashcardChoice in rFlashcardChoices)
            {
                flashcardChoices.Add(new FlashcardChoiceDTO()
                {
                    FlashcardId = rFlashcardChoice.FlashcardId,
                    Flashcard = rFlashcardChoice.Flashcard,
                    Choice = rFlashcardChoice.Choice,
                    IsAnswer = rFlashcardChoice.IsAnswer,
                    DateAdded = rFlashcardChoice.DateAdded,
                    DateUpdated = rFlashcardChoice.DateUpdated
                });
            }

            return flashcardChoices;
        }
        public FlashcardChoiceDTO? GetFlashcardChoiceById(int id)
        {
            var rFlashcardChoice = _repository.GetFlashcardChoiceById(id);

            if(rFlashcardChoice == null)
                return null;

            return new FlashcardChoiceDTO()
            {
                FlashcardId = rFlashcardChoice.FlashcardId,
                Flashcard = rFlashcardChoice.Flashcard,
                Choice = rFlashcardChoice.Choice,
                IsAnswer = rFlashcardChoice.IsAnswer,
                DateAdded = rFlashcardChoice.DateAdded,
                DateUpdated = rFlashcardChoice.DateUpdated
            };
        }

        public List<FlashcardChoiceDTO>? GetAllFlashcardChoicesByFlashcardID(int fcid)
        {
            List<FlashcardChoiceDTO> flashcardChoices = new List<FlashcardChoiceDTO>();
            var rFlashcardChoices = _repository.GetAllFlashcardChoicesByFlashcardID(fcid);

            foreach (var rFlashcardChoice in rFlashcardChoices)
            {
                flashcardChoices.Add(new FlashcardChoiceDTO()
                {
                    FlashcardId = rFlashcardChoice.FlashcardId,
                    Flashcard = rFlashcardChoice.Flashcard,
                    Choice = rFlashcardChoice.Choice,
                    IsAnswer = rFlashcardChoice.IsAnswer,
                    DateAdded = rFlashcardChoice.DateAdded,
                    DateUpdated = rFlashcardChoice.DateUpdated
                });
            }

            return flashcardChoices;
        }
    }
}
