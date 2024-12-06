using RestTest.DTO;

namespace RestTest.Services.IServices
{
    public interface IFlashcardSetService
    {
        public List<FlashcardSetDTO> GetAllFlashcardSets();
        public List<FlashcardSetDTO>? GetAllFlashcardSetsByUserId(int uid);

        public FlashcardSetDTO? GetFlashcardSetById(int fcsid, int uid);
    }
}
