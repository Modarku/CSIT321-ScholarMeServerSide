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

        public async Task<List<FlashcardDeckReadOnlyDto>> GetFlashcardSets(int userAccountId)
        {
            List<FlashcardDeckReadOnlyDto> result = new List<FlashcardDeckReadOnlyDto>();

            var flashcardSets = await _flashcardSetInfoRepository.GetFlashcardSets(userAccountId);

            foreach (FlashcardDeck flashcardSet in flashcardSets)
            {
                result.Add(new FlashcardDeckReadOnlyDto()
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
        public async Task<FlashcardDeckReadOnlyDto?> GetFlashcardSetById(int id)
        {
            var flashcardSet = await _flashcardSetInfoRepository.GetFlashcardSetById(id);

            if (flashcardSet == null)
            {
                return null;
            }

            return new FlashcardDeckReadOnlyDto()
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

        public async Task<FlashcardDeckReadOnlyDto> CreateFlashcardSet(FlashcardDeckNewDto flashcardSetDto)
        {
            if (!_flashcardSetInfoRepository.UserAccountExists(flashcardSetDto.UserAccountId))
            {
                throw new ArgumentException($"User account with id {flashcardSetDto.UserAccountId} does not exist.");
            }

            var flashcard = await _flashcardSetInfoRepository.CreateFlashcardSet(new FlashcardDeck()
            {
                UserAccountId = flashcardSetDto.UserAccountId,
                Title = flashcardSetDto.Title,
                Description = flashcardSetDto.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            return new FlashcardDeckReadOnlyDto()
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

        public async Task<FlashcardDeckReadOnlyDto?> UpdateFlashcardSet(int id, FlashcardDeckUpdateDto flashcardSetDto)
        {
            var flashcardSet = await _flashcardSetInfoRepository.UpdateFlashcardSet(id, new FlashcardDeck()
            {
                Title = flashcardSetDto.Title,
                Description = flashcardSetDto.Description
            });

            if (flashcardSet == null)
            {
                return null;
            }

            return new FlashcardDeckReadOnlyDto()
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
