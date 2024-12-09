using RestTest.Models;
using ScholarMeServer.DTO.Flashcard;
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
                FlashcardSetId = flashcardDeckId,
                Question = flashcardDto.Question,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _flashcardRepository.AddFlashcard(flashcard);

            return new FlashcardReadOnlyDto
            {
                Id = flashcard.Id,
                FlashcardSetId = flashcard.FlashcardSetId,
                Question = flashcard.Question,
            };
        }

        public async Task<List<FlashcardReadOnlyDto>> GetFlashcardsByDeckId(int flashcardDeckId)
        {
            var flashcard = await _flashcardRepository.GetFlashcardsByDeckId(flashcardDeckId);

            return flashcard.Select(f => new FlashcardReadOnlyDto
            {
                Id = f.Id,
                FlashcardSetId = f.FlashcardSetId,
                Question = f.Question,
            }).ToList();
        }

        public async Task<FlashcardReadOnlyDto> UpdateFlashcard(int flashcardDeckId, FlashcardUpdateDto flashcardDto)
        {
            var flashcard = await _flashcardRepository.GetFlashcardById(flashcardDeckId);

            if (flashcard == null)
            {
                // TODO:
                throw new NotImplementedException("Flashcard Not Found: Validation logic not yet implemented!");
            }

            if (flashcardDto.FlashcardSetId.HasValue && flashcardDto.FlashcardSetId != 0)
            {
                if (!await _flashcardRepository.FlashcardDeckExists((int)flashcardDto.FlashcardSetId))
                {
                    // TODO:
                    throw new NotImplementedException("Flashcard Deck Not Found: Validation logic not yet implemented!");
                }
                flashcard.FlashcardSetId = (int)flashcardDto.FlashcardSetId;
            }

            if (!string.IsNullOrEmpty(flashcardDto.Question))
            {
                flashcard.Question = flashcardDto.Question;
            }

            flashcard.UpdatedAt = DateTime.UtcNow;
            await _flashcardRepository.SaveFlashcard(flashcard);

            return new FlashcardReadOnlyDto
            {
                Id = flashcard.Id,
                FlashcardSetId = flashcard.FlashcardSetId,
                Question = flashcard.Question,
            };
        }

        public async Task DeleteFlashcard(int flashcardDeckId)
        {
            await _flashcardRepository.DeleteFlashcard(flashcardDeckId);
        }
    }
}
