using RestTest.Models;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.DTO.FlashcardChoice;
using ScholarMeServer.Repository.FlashcardInfo;

namespace ScholarMeServer.Services.FlashcardInfo
{
    public class FlashcardInfoService : IFlashcardInfoService
    {
        private readonly IFlashcardInfoRepository _flashcardInfoRepository;

        public FlashcardInfoService(IFlashcardInfoRepository flashcardInfoRepository)
        {
            _flashcardInfoRepository = flashcardInfoRepository;
        }

        public async Task<IEnumerable<FlashcardDto>> GetFlashcardsAsync()
        {
            var flashcards = await _flashcardInfoRepository.GetFlashcardsAsync();
            return flashcards.Select(f => new FlashcardDto
            {
                Id = f.Id,
                Question = f.Question,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt,
                Choices = f.Choices?.Select(c => new FlashcardChoiceInnerDto
                {
                    Id = c.Id,
                    Choice = c.Choice,
                    IsAnswer = c.IsAnswer,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                }).ToList()
            });
        }

        public async Task<FlashcardDto?> GetFlashcardByIdAsync(int id)
        {
            var flashcard = await _flashcardInfoRepository.GetFlashcardByIdAsync(id);

            if (flashcard == null)
            {
                return null;
            }
            return new FlashcardDto
            {
                Id = flashcard.Id,
                Question = flashcard.Question,
                CreatedAt = flashcard.CreatedAt,
                UpdatedAt = flashcard.UpdatedAt,
                Choices = flashcard.Choices?.Select(c => new FlashcardChoiceInnerDto
                {
                    Id = c.Id,
                    Choice = c.Choice,
                    IsAnswer = c.IsAnswer,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                }).ToList(),
            };
        }

        public async Task<FlashcardDto> CreateFlashcardAsync(FlashcardCreateDto flashcardDto)
        {
            var createdFlashcard = await _flashcardInfoRepository.CreateFlashcardAsync(
                    new Flashcard()
                    {
                        Question = flashcardDto.Question,
                    }
                );

            return new FlashcardDto
            {
                Id = createdFlashcard.Id,
                Question = createdFlashcard.Question,
                CreatedAt = createdFlashcard.CreatedAt,
                UpdatedAt = createdFlashcard.UpdatedAt,
                Choices = []
            };
        }

        public async Task<FlashcardDto?> UpdateFlashcardAsync(int id, FlashcardCreateDto flashcardDto)
        {
            var flashcard = await _flashcardInfoRepository.UpdateFlashcardAsync(
                    id,
                    new Flashcard()
                    {
                        Question = flashcardDto.Question,
                    }
                );

            if (flashcard == null)
            {
                return null;
            }

            return new FlashcardDto()
            {
                Id = flashcard.Id,
                Question = flashcard.Question,
                CreatedAt = flashcard.CreatedAt,
                UpdatedAt = flashcard.UpdatedAt,
                Choices = flashcard.Choices?.Select(c => new FlashcardChoiceInnerDto
                {
                    Id = c.Id,
                    Choice = c.Choice,
                    IsAnswer = c.IsAnswer,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                }).ToList(),
            };
        }

        public async Task DeleteFlashcardAsync(int id)
        {
            await _flashcardInfoRepository.DeleteFlashcardAsync(id);
        }
    }
}
