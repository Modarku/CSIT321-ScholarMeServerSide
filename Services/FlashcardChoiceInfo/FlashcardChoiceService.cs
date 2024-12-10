using RestTest.Models;
using ScholarMeServer.DTO.FlashcardChoice;
using ScholarMeServer.Repository.FlashcardChoiceInfo;

namespace ScholarMeServer.Services.FlashcardChoiceInfo
{
    public class FlashcardChoiceService : IFlashcardChoiceService
    {
        private readonly IFlashcardChoiceRepository _flashcardChoiceRepository;

        public FlashcardChoiceService(IFlashcardChoiceRepository flashcardChoiceRepository)
        {
            _flashcardChoiceRepository = flashcardChoiceRepository;
        }

        public async Task<FlashcardChoiceReadOnlyDto> CreateFlashcardChoice(int flashcardId, FlashcardChoiceCreateDto flashcardChoiceDto)
        {
            FlashcardChoice flashcardChoice = new FlashcardChoice()
            {
                FlashcardId = flashcardId,
                Choice = flashcardChoiceDto.Choice,
                IsAnswer = flashcardChoiceDto.IsAnswer,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _flashcardChoiceRepository.AddFlashcardChoice(flashcardChoice);

            return new FlashcardChoiceReadOnlyDto()
            {
                Id = flashcardChoice.Id,
                FlashcardId = flashcardChoice.FlashcardId,
                Choice = flashcardChoice.Choice,
                IsAnswer = flashcardChoice.IsAnswer,
                CreatedAt = flashcardChoice.CreatedAt,
                UpdatedAt = flashcardChoice.UpdatedAt
            };
        }

        public async Task<List<FlashcardChoiceReadOnlyDto>> GetFlashcardChoicesByCardId(int flashcardId)
        {
            var flashcardChoices = await _flashcardChoiceRepository.GetFlashcardChoicesByCardId(flashcardId);

            return flashcardChoices.Select(c => new FlashcardChoiceReadOnlyDto()
            {
                Id = c.Id,
                FlashcardId = c.FlashcardId,
                Choice = c.Choice,
                IsAnswer = c.IsAnswer,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }

        public async Task<FlashcardChoiceReadOnlyDto> GetFlashcardChoiceById(int flashcardChoiceId)
        {
            var flashcardChoice = await _flashcardChoiceRepository.GetFlashcardChoiceById(flashcardChoiceId);

            if (flashcardChoice == null)
            {
                // TODO:
                throw new NotImplementedException("Flashcard Choice Not Found: Validation logic is not yet implemented!");
            }

            return new FlashcardChoiceReadOnlyDto()
            {
                Id = flashcardChoice.Id,
                FlashcardId = flashcardChoice.FlashcardId,
                Choice = flashcardChoice.Choice,
                IsAnswer = flashcardChoice.IsAnswer,
                CreatedAt = flashcardChoice.CreatedAt,
                UpdatedAt = flashcardChoice.UpdatedAt
            };
        }

        public async Task<FlashcardChoiceReadOnlyDto> UpdateFlashcardChoice(int flashcardChoiceId, FlashcardChoiceUpdateDto flashcardChoiceDto)
        {
            var flashcardChoice = await _flashcardChoiceRepository.GetFlashcardChoiceById(flashcardChoiceId);

            if (flashcardChoice == null)
            {
                throw new NotImplementedException("Flashcard Choice Not Found: Validation logic is not yet implemented!");
            }

            if (flashcardChoiceDto.Choice != null)
            {
                flashcardChoice.Choice = flashcardChoiceDto.Choice;
            }
            if (flashcardChoiceDto.IsAnswer != null)
            {
                flashcardChoice.IsAnswer = (bool)flashcardChoiceDto.IsAnswer;
            }

            flashcardChoice.UpdatedAt = DateTime.UtcNow;
            await _flashcardChoiceRepository.SaveFlashcardChoice(flashcardChoice);

            return new FlashcardChoiceReadOnlyDto()
            {
                Id = flashcardChoice.Id,
                FlashcardId = flashcardChoice.FlashcardId,
                Choice = flashcardChoice.Choice,
                IsAnswer = flashcardChoice.IsAnswer,
                CreatedAt = flashcardChoice.CreatedAt,
                UpdatedAt = flashcardChoice.UpdatedAt
            };
        }
        public async Task DeleteFlashcardChoice(int flashcardChoiceId)
        {
            var flashcardChoice = await _flashcardChoiceRepository.GetFlashcardChoiceById(flashcardChoiceId);

            if (flashcardChoice == null)
            {
                throw new NotImplementedException("Flashcard Choice Not Found: Validation logic is not yet implemented!");
            }

            await _flashcardChoiceRepository.DeleteFlashcardChoice(flashcardChoice);
        }
    }
}
