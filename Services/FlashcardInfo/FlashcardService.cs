using RestTest.Models;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.DTO.FlashcardChoice;
using ScholarMeServer.Repository.FlashcardInfo;

namespace ScholarMeServer.Services.FlashcardInfo
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IFlashcardRepository _flashcardRepository;

        public FlashcardService(IFlashcardRepository flashcardRepository)
        {
            _flashcardRepository = flashcardRepository;
        }

        public async Task<FlashcardReadOnlyDto> CreateFlashcard(int flashcardDeckId, FlashcardCreateDto flashcardDto)
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

        public async Task<List<FlashcardReadOnlyDto>> GetFlashcardsByDeckId(int flashcardDeckId)
        {
            var flashcards = await _flashcardRepository.GetFlashcardsByDeckId(flashcardDeckId);

            return flashcards.Select(f => new FlashcardReadOnlyDto
            {
                Id = f.Id,
                FlashcardSetId = f.FlashcardDeckId,
                Question = f.Question,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt,
            }).ToList();
        }

        public async Task<FlashcardReadOnlyDto> GetFlashcardById(int flashcardId)
        {
            var flashcard = await _flashcardRepository.GetFlashcardById(flashcardId);

            if (flashcard == null)
            {
                // TODO:
                throw new NotImplementedException("Flashcard Not Found: Validation logic not yet implemented!");
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

        public async Task<FlashcardReadOnlyDto> UpdateFlashcard(int flashcardId, FlashcardUpdateDto flashcardDto)
        {
            var flashcard = await _flashcardRepository.GetFlashcardById(flashcardId);

            if (flashcard == null)
            {
                // TODO:
                throw new NotImplementedException("Flashcard Not Found: Validation logic not yet implemented!");
            }

            if (flashcardDto.FlashcardSetId != null)
            {
                if (!await _flashcardRepository.FlashcardDeckExists((int)flashcardDto.FlashcardSetId))
                {
                    // TODO:
                    throw new NotImplementedException("Flashcard FlashcardDeck Not Found: Validation logic not yet implemented!");
                }
                flashcard.FlashcardDeckId = (int)flashcardDto.FlashcardSetId;
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

        public async Task DeleteFlashcard(int flashcardId)
        {
            var flashcard = await _flashcardRepository.GetFlashcardById(flashcardId);

            if (flashcard == null)
            {
                // TODO:
                throw new NotImplementedException("Flashcard Not Found: Validation logic not yet implemented!");
            }

            await _flashcardRepository.DeleteFlashcard(flashcard);
        }
    }
}
