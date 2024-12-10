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

            await _flashcardDeckRepository.AddFlashcardDeck(flashcardDeck);

            return new FlashcardDeckReadOnlyDto()
            {
                Id = flashcardDeck.Id,
                UserAccountId = flashcardDeck.UserAccountId,
                Title = flashcardDeck.Title,
                Description = flashcardDeck.Description,
                CreatedAt = flashcardDeck.CreatedAt,
                UpdatedAt = flashcardDeck.UpdatedAt,
            };
        } 

        public async Task<List<FlashcardDeckReadOnlyDto>> GetFlashcardDecksByUserId(int userAccountId)
        {
            var flashcardDecks = await _flashcardDeckRepository.GetFlashcardDecksByUserId(userAccountId);

            return flashcardDecks.Select(d => new FlashcardDeckReadOnlyDto()
            {
                Id = d.Id,
                UserAccountId = d.UserAccountId,
                Title = d.Title,
                Description = d.Description,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt,
            }).ToList();
        }

        public async Task<FlashcardDeckReadOnlyDto> GetFlashcardDeckById(int flashcardDeckId)
        {
            var flashcardDeck = await _flashcardDeckRepository.GetFlashcardDeckById(flashcardDeckId);

            if (flashcardDeck == null)
            {
                // TODO:
                throw new NotImplementedException("Flashcard Deck Not Found: Validation logic not yet implemented!");
            }

            return new FlashcardDeckReadOnlyDto()
            {
                Id = flashcardDeck.Id,
                UserAccountId = flashcardDeck.UserAccountId,
                Title = flashcardDeck.Title,
                Description = flashcardDeck.Description,
                CreatedAt = flashcardDeck.CreatedAt,
                UpdatedAt = flashcardDeck.UpdatedAt,
            };
        }

        public async Task<FlashcardDeckReadOnlyDto> UpdateFlashcardDeck(int flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto)
        {
            var flashcardDeck = await _flashcardDeckRepository.GetFlashcardDeckById(flashcardDeckId);

            if (flashcardDeck == null)
            {
                // TODO:
                throw new NotImplementedException("Flashcard FlashcardDeck Not Found: Validation logic not yet implemented!");
            }

            if (flashcardDeckDto.Title != null)
            {
                flashcardDeck.Title = flashcardDeckDto.Title;
            }

            if (flashcardDeckDto.Description != null)
            {
                flashcardDeck.Description = flashcardDeckDto.Description;
            }

            flashcardDeck.UpdatedAt = DateTime.UtcNow;

            await _flashcardDeckRepository.SaveFlashcardDeck(flashcardDeck);

            return new FlashcardDeckReadOnlyDto()
            {
                Id = flashcardDeck.Id,
                UserAccountId = flashcardDeck.UserAccountId,
                Title = flashcardDeck.Title,
                Description = flashcardDeck.Description,
                CreatedAt = flashcardDeck.CreatedAt,
                UpdatedAt = flashcardDeck.UpdatedAt,
            };
        }

        public async Task DeleteFlashcardDeck(int flashcardDeckId)
        {
            var flashcardDeck = await _flashcardDeckRepository.GetFlashcardDeckById(flashcardDeckId);

            if (flashcardDeck == null) 
            {
                // TODO:
                throw new NotImplementedException("Flashcard Deck Not Found: Validation logic not yet implemented!");
            }

            await _flashcardDeckRepository.DeleteFlashcardDeck(flashcardDeck);
        }
    }
}
