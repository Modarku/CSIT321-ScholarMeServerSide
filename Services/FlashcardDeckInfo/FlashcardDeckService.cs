using RestTest.Models;
using ScholarMeServer.DTO.FlashcardDeck;
using ScholarMeServer.Repository.FlashcardDeckInfo;

namespace ScholarMeServer.Services.FlashcardDeckInfo
{
    public class FlashcardDeckService : IFlashcardDeckService
    {
        private readonly IFlashcardDeckRepository _flashcardDeckRepository;

        public FlashcardDeckService(IFlashcardDeckRepository flashcardDeckRepository)
        {
            _flashcardDeckRepository = flashcardDeckRepository;
        }

        public async Task<FlashcardDeckReadOnlyDto> CreateFlashcardDeck(int userAccountId, FlashcardDeckCreateDto flashcardDeckDto)
        {
            FlashcardDeck flashcardDeck = new FlashcardDeck()
            {
                UserAccountId = userAccountId,
                Title = flashcardDeckDto.Title,
                Description = flashcardDeckDto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            FlashcardDeck createdFlashcardDeck = await _flashcardDeckRepository.CreateFlashcardDeck(flashcardDeck);

            return new FlashcardDeckReadOnlyDto()
            {
                Id = createdFlashcardDeck.Id,
                UserAccountId = createdFlashcardDeck.UserAccountId,
                Title = createdFlashcardDeck.Title,
                Description = createdFlashcardDeck.Description,
                CreatedAt = createdFlashcardDeck.CreatedAt,
                UpdatedAt = createdFlashcardDeck.UpdatedAt,
            };
        } 

        public async Task<List<FlashcardDeckReadOnlyDto>> GetFlashcardDecks(int userAccountId)
        {
            var flashcardDecks = await _flashcardDeckRepository.GetFlashcardDecks(userAccountId);

            return flashcardDecks.Select(deck => new FlashcardDeckReadOnlyDto()
            {
                Id = deck.Id,
                UserAccountId = deck.UserAccountId,
                Title = deck.Title,
                Description = deck.Description,
                CreatedAt = deck.CreatedAt,
                UpdatedAt = deck.UpdatedAt,
            }).ToList();
        }

        public async Task<FlashcardDeckReadOnlyDto> UpdateFlashcardDeck(int flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto)
        {
            var existingFlashcardDeck = await _flashcardDeckRepository.GetFlashcardDeckById(flashcardDeckId);

            if (existingFlashcardDeck == null)
            {
                throw new NotImplementedException("Flashcard Deck Not Found: Validation logic not yet implemented!");
            }

            if (!string.IsNullOrEmpty(flashcardDeckDto.Title))
            {
                existingFlashcardDeck.Title = flashcardDeckDto.Title;
            }

            if (!string.IsNullOrEmpty(flashcardDeckDto.Description))
            {
                existingFlashcardDeck.Description = flashcardDeckDto.Description;
            }

            existingFlashcardDeck.UpdatedAt = DateTime.UtcNow;

            await _flashcardDeckRepository.SaveFlashcardDeck(existingFlashcardDeck);

            return new FlashcardDeckReadOnlyDto()
            {
                Id = existingFlashcardDeck.Id,
                UserAccountId = existingFlashcardDeck.UserAccountId,
                Title = existingFlashcardDeck.Title,
                Description = existingFlashcardDeck.Description,
                CreatedAt = existingFlashcardDeck.CreatedAt,
                UpdatedAt = existingFlashcardDeck.UpdatedAt,
            };
        }

        public async Task DeleteFlashcardDeck(int flashcardDeckId)
        {
            await _flashcardDeckRepository.DeleteFlashcardDeck(flashcardDeckId);
        }
    }
}
