using RestTest.Models;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.DTO.FlashcardSet;
using ScholarMeServer.Repository.FlashcardSetInfo;

namespace ScholarMeServer.Services.FlashcardSetInfo
{
    public class FlashcardSetInfoService : IFlashcardSetInfoService
    {
        private readonly IFlashcardSetInfoRepository _flashcardSetInfoRepository;

        public FlashcardSetInfoService(IFlashcardSetInfoRepository flashcardSetInfoRepository)
        {
            _flashcardSetInfoRepository = flashcardSetInfoRepository;
        }

        public async Task<List<FlashcardSetDto>> GetFlashcardSets(int userAccountId)
        {
            List<FlashcardSetDto> result = new List<FlashcardSetDto>();

            var flashcardSets = await _flashcardSetInfoRepository.GetFlashcardSets(userAccountId);

            foreach (FlashcardSet flashcardSet in flashcardSets)
            {
                result.Add(new FlashcardSetDto()
                {
                    Id = flashcardSet.Id,
                    UserAccountId = flashcardSet.UserAccountId,
                    Title = flashcardSet.Title,
                    Description = flashcardSet.Description,
                    CreatedAt = flashcardSet.CreatedAt,
                    UpdatedAt = flashcardSet.UpdatedAt,
                    Flashcards = flashcardSet.Flashcards?.Select(flashcard => new FlashcardInnerDto()
                    {
                        Id = flashcard.Id,
                        Question = flashcard.Question,
                        CreatedAt = flashcard.CreatedAt,
                        UpdatedAt = flashcard.UpdatedAt
                    }).ToList() ?? new List<FlashcardInnerDto>()
                });
            }

            return result;

        }
        public async Task<FlashcardSetDto?> GetFlashcardSetById(int id)
        {
            var flashcardSet = await _flashcardSetInfoRepository.GetFlashcardSetById(id);

            if (flashcardSet == null)
            {
                return null;
            }

            return new FlashcardSetDto()
            {
                Id = flashcardSet.Id,
                UserAccountId = flashcardSet.UserAccountId,
                Title = flashcardSet.Title,
                Description = flashcardSet.Description,
                CreatedAt = flashcardSet.CreatedAt,
                UpdatedAt = flashcardSet.UpdatedAt,
                Flashcards = flashcardSet.Flashcards?.Select(flashcard => new FlashcardInnerDto()
                {
                    Id = flashcard.Id,
                    Question = flashcard.Question,
                    CreatedAt = flashcard.CreatedAt,
                    UpdatedAt = flashcard.UpdatedAt
                }).ToList() ?? []
            };
        }

        public async Task<FlashcardSetDto> CreateFlashcardSet(FlashcardSetCreateDto flashcardSetDto)
        {
            if (!_flashcardSetInfoRepository.UserAccountExists(flashcardSetDto.UserAccountId))
            {
                throw new ArgumentException($"User account with id {flashcardSetDto.UserAccountId} does not exist.");
            }

            var flashcard = await _flashcardSetInfoRepository.CreateFlashcardSet(new FlashcardSet()
            {
                UserAccountId = flashcardSetDto.UserAccountId,
                Title = flashcardSetDto.Title,
                Description = flashcardSetDto.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            return new FlashcardSetDto()
            {
                Id = flashcard.Id,
                UserAccountId = flashcard.UserAccountId,
                Title = flashcard.Title,
                Description = flashcard.Description,
                CreatedAt = flashcard.CreatedAt,
                UpdatedAt = flashcard.UpdatedAt,
                Flashcards = new List<FlashcardInnerDto>()
            };
        }

        public async Task<FlashcardSetDto?> UpdateFlashcardSet(int id, FlashcardSetUpdateDto flashcardSetDto)
        {
            var flashcardSet = await _flashcardSetInfoRepository.UpdateFlashcardSet(id, new FlashcardSet()
            {
                Title = flashcardSetDto.Title,
                Description = flashcardSetDto.Description
            });

            if (flashcardSet == null)
            {
                return null;
            }

            return new FlashcardSetDto()
            {
                Id = flashcardSet.Id,
                UserAccountId = flashcardSet.UserAccountId,
                Title = flashcardSet.Title,
                Description = flashcardSet.Description,
                CreatedAt = flashcardSet.CreatedAt,
                UpdatedAt = flashcardSet.UpdatedAt,
                Flashcards = flashcardSet.Flashcards?.Select(flashcard => new FlashcardInnerDto()
                {
                    Id = flashcard.Id,
                    Question = flashcard.Question,
                    CreatedAt = flashcard.CreatedAt,
                    UpdatedAt = flashcard.UpdatedAt
                }).ToList() ?? []
            };
        }
        public async Task DeleteFlashcardSet(int id)
        {
            await _flashcardSetInfoRepository.DeleteFlashcardSet(id);
        }

    }
}
