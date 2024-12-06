using RestTest.DTO;
using RestTest.Models;
using RestTest.Repository.IRepository;
using RestTest.Services.IServices;

namespace RestTest.Services
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IFlashcardRepository _repository;
        public FlashcardService(IFlashcardRepository repository)
        {
            _repository = repository;
        }

        public List<FlashcardDTO> GetAllFlashcards()
        {
            List<FlashcardDTO> flashcards = new List<FlashcardDTO>();
            var rFlashcards = _repository.GetAllFlashcards();

            foreach (var rFlashcard in rFlashcards)
            {
                flashcards.Add(new FlashcardDTO()
                {
                    UserId = rFlashcard.UserId,
                    UserAccount = rFlashcard.UserAccount,
                    Question = rFlashcard.Question,
                    DateAdded = rFlashcard.DateAdded,
                    DateUpdated = rFlashcard.DateUpdated
                });
            }

            return flashcards;
        }

        public List<FlashcardDTO> GetAllFlashcardsBySetId(int fcsid)
        {
            List<FlashcardDTO> flashcards = new List<FlashcardDTO>();
            var rFlashcards = _repository.GetAllFlashcardsBySetId(fcsid);

            foreach (var rFlashcard in rFlashcards)
            {
                flashcards.Add(new FlashcardDTO()
                {
                    UserId = rFlashcard.UserId,
                    UserAccount = rFlashcard.UserAccount,
                    Question = rFlashcard.Question,
                    DateAdded = rFlashcard.DateAdded,
                    DateUpdated = rFlashcard.DateUpdated
                });
            }

            return flashcards;

            
        }
        public FlashcardDTO? GetFlashcardById(int fcsid, int fcid)
        {
            var rFlashcard = _repository.GetFlashcardById(fcsid, fcid);

            if (rFlashcard == null)
                return null;

            return new FlashcardDTO()
            {
                UserId = rFlashcard.UserId,
                UserAccount = rFlashcard.UserAccount,
                Question = rFlashcard.Question,
                DateAdded = rFlashcard.DateAdded,
                DateUpdated = rFlashcard.DateUpdated
            };
        }
    }
}
