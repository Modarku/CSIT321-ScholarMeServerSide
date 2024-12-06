using RestTest.DTO;
using RestTest.Models;
using RestTest.Repository.IRepository;
using RestTest.Services.IServices;

namespace RestTest.Services
{
    public class FlashcardSetService : IFlashcardSetService
    {
        private readonly IFlashcardSetRepository _repository;
        public FlashcardSetService(IFlashcardSetRepository repository)
        {
            _repository = repository;
        }

        public List<FlashcardSetDTO> GetAllFlashcardSets()
        {
            List<FlashcardSetDTO> flashcardSets = new List<FlashcardSetDTO>();
            var rFlashcardSets = _repository.GetAllFlashcardSets();

            foreach (var rFlashcardSet in rFlashcardSets)
            {
                flashcardSets.Add(new FlashcardSetDTO()
                {
                    UserId = rFlashcardSet.UserId,
                    UserAccount = rFlashcardSet.UserAccount,
                    Title = rFlashcardSet.Title,
                    Description = rFlashcardSet.Description,
                    DateAdded = rFlashcardSet.DateAdded,
                    DateUpdated = rFlashcardSet.DateUpdated
                });
            }

            return flashcardSets;
        }
        public List<FlashcardSetDTO>? GetAllFlashcardSetsByUserId(int uid)
        {
            List<FlashcardSetDTO> flashcardSets = new List<FlashcardSetDTO>();
            var rFlashcardSets = _repository.GetAllFlashcardSetsByUserId(uid);

            foreach (var rFlashcardSet in rFlashcardSets)
            {
                flashcardSets.Add(new FlashcardSetDTO()
                {
                    UserId = rFlashcardSet.UserId,
                    UserAccount = rFlashcardSet.UserAccount,
                    Title = rFlashcardSet.Title,
                    Description = rFlashcardSet.Description,
                    DateAdded = rFlashcardSet.DateAdded,
                    DateUpdated = rFlashcardSet.DateUpdated
                });
            }

            return flashcardSets;
        }

        public FlashcardSetDTO? GetFlashcardSetById(int fcsid, int uid)
        {
            var rFlashcardSet = _repository.GetFlashcardSetById(fcsid, uid);

            if (rFlashcardSet == null)
                return null;

            return new FlashcardSetDTO()
            {
                UserId = rFlashcardSet.UserId,
                UserAccount = rFlashcardSet.UserAccount,
                Title = rFlashcardSet.Title,
                Description = rFlashcardSet.Description,
                DateAdded = rFlashcardSet.DateAdded,
                DateUpdated = rFlashcardSet.DateUpdated
            };
        }
        
    }
}
