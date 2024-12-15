using RestTest.Models;
using ScholarMeServer.DTO.FlashcardChoice;
using ScholarMeServer.Repository.FlashcardChoiceInfo;
using ScholarMeServer.Utilities.Exceptions;
using System.Net;

namespace ScholarMeServer.Services.FlashcardChoiceInfo
{
    public class FlashcardChoiceService : IFlashcardChoiceService
    {
        private readonly IFlashcardChoiceRepository _flashcardChoiceRepository;

        public FlashcardChoiceService(IFlashcardChoiceRepository flashcardChoiceRepository)
        {
            _flashcardChoiceRepository = flashcardChoiceRepository;
        }

        public async Task<FlashcardChoiceReadOnlyDto> CreateFlashcardChoice(Guid flashcardId, FlashcardChoiceCreateDto flashcardChoiceDto)
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

        public async Task<List<FlashcardChoiceReadOnlyDto>> GetFlashcardChoicesByCardId(Guid flashcardId)
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

        public async Task<FlashcardChoiceReadOnlyDto> GetFlashcardChoiceById(Guid flashcardChoiceId)
        {
            var flashcardChoice = await _flashcardChoiceRepository.GetFlashcardChoiceById(flashcardChoiceId);

            if (flashcardChoice == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard choice not found");
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

        public async Task<FlashcardChoiceReadOnlyDto> UpdateFlashcardChoice(Guid flashcardChoiceId, FlashcardChoiceUpdateDto flashcardChoiceDto)
        {
            var flashcardChoice = await _flashcardChoiceRepository.GetFlashcardChoiceById(flashcardChoiceId);

            if (flashcardChoice == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard choice not found");
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
        public async Task DeleteFlashcardChoice(Guid flashcardChoiceId)
        {
            var flashcardChoice = await _flashcardChoiceRepository.GetFlashcardChoiceById(flashcardChoiceId);

            if (flashcardChoice == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard choice not found");
            }

            await _flashcardChoiceRepository.DeleteFlashcardChoice(flashcardChoice);
        }
    }
}
