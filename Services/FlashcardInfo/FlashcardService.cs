using RestTest.Models;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.DTO.FlashcardChoice;
using ScholarMeServer.Repository.FlashcardInfo;
using ScholarMeServer.Utilities.Exceptions;
using System.Net;

namespace ScholarMeServer.Services.FlashcardInfo
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IFlashcardRepository _flashcardRepository;

        public FlashcardService(IFlashcardRepository flashcardRepository)
        {
            _flashcardRepository = flashcardRepository;
        }

        public async Task<FlashcardReadOnlyDto> CreateFlashcard(Guid flashcardDeckId, FlashcardCreateDto flashcardDto)
        {
            Flashcard flashcard = new Flashcard
            {
                FlashcardDeckId = flashcardDeckId,
                Question = flashcardDto.Question,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _flashcardRepository.AddFlashcard(flashcard);

            return new FlashcardReadOnlyDto
            {
                Id = flashcard.Id,
                FlashcardSetId = flashcard.FlashcardDeckId,
                Question = flashcard.Question,
            };
        }

        public async Task<List<FlashcardReadOnlyDto>> GetFlashcardsByDeckId(Guid flashcardDeckId, bool includeChoices)
        {
            var flashcards = await _flashcardRepository.GetFlashcardsByDeckId(flashcardDeckId, includeChoices);

            return flashcards.Select(f => new FlashcardReadOnlyDto
            {
                Id = f.Id,
                FlashcardSetId = f.FlashcardDeckId,
                Question = f.Question,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt,
                Choices = includeChoices ? f.Choices.Select(c => new FlashcardChoiceReadOnlyDto
                {
                    Id = c.Id,
                    FlashcardId = null,
                    Choice = c.Choice,
                    IsAnswer = c.IsAnswer,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                }).ToList() : null
            }).ToList();
        }

        public async Task<FlashcardReadOnlyDto> GetFlashcardById(Guid flashcardId, bool includeChoices)
        {
            var flashcard = await _flashcardRepository.GetFlashcardById(flashcardId, includeChoices);

            if (flashcard == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard not found");
            }

            return new FlashcardReadOnlyDto
            {
                Id = flashcard.Id,
                FlashcardSetId = flashcard.FlashcardDeckId,
                Question = flashcard.Question,
                CreatedAt = flashcard.CreatedAt,
                UpdatedAt = flashcard.UpdatedAt,
            };
        }

        public async Task<FlashcardReadOnlyDto> UpdateFlashcard(Guid flashcardId, FlashcardUpdateDto flashcardDto)
        {
            var flashcard = await _flashcardRepository.GetFlashcardById(flashcardId);

            if (flashcard == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard not found");
            }

            if (flashcardDto.FlashcardSetId != null)
            {
                if (!await _flashcardRepository.FlashcardDeckExists((Guid)flashcardDto.FlashcardSetId))
                {
                    // TODO:
                    throw new NotImplementedException("Flashcard FlashcardDeck Not Found: Validation logic not yet implemented!");
                }
                flashcard.FlashcardDeckId = (Guid)flashcardDto.FlashcardSetId;
            }

            if (flashcardDto.Question != null)
            {
                flashcard.Question = flashcardDto.Question;
            }

            flashcard.UpdatedAt = DateTime.UtcNow;
            await _flashcardRepository.SaveFlashcard(flashcard);

            return new FlashcardReadOnlyDto
            {
                Id = flashcard.Id,
                FlashcardSetId = flashcard.FlashcardDeckId,
                Question = flashcard.Question,
            };
        }

        public async Task DeleteFlashcard(Guid flashcardId)
        {
            var flashcard = await _flashcardRepository.GetFlashcardById(flashcardId);

            if (flashcard == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard not found");
            }

            await _flashcardRepository.DeleteFlashcard(flashcard);
        }
    }
}
