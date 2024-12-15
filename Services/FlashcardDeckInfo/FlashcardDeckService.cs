using RestTest.Models;
using ScholarMeServer.DTO.FlashcardDeck;
using ScholarMeServer.Repository.FlashcardDeckInfo;
using ScholarMeServer.Utilities.Exceptions;
using System.Net;

namespace ScholarMeServer.Services.FlashcardDeckInfo
{
    public class FlashcardDeckService : IFlashcardDeckService
    {
        private readonly IFlashcardDeckRepository _flashcardDeckRepository;

        public FlashcardDeckService(IFlashcardDeckRepository flashcardDeckRepository)
        {
            _flashcardDeckRepository = flashcardDeckRepository;
        }

        public async Task<FlashcardDeckReadOnlyDto> CreateFlashcardDeck(Guid userAccountId, FlashcardDeckCreateDto flashcardDeckDto)
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

        public async Task<List<FlashcardDeckReadOnlyDto>> GetFlashcardDecksByUserId(Guid userAccountId)
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

        public async Task<FlashcardDeckReadOnlyDto> GetFlashcardDeckById(Guid flashcardDeckId)
        {
            var flashcardDeck = await _flashcardDeckRepository.GetFlashcardDeckById(flashcardDeckId);

            if (flashcardDeck == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard deck not found");
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

        public async Task<FlashcardDeckReadOnlyDto> UpdateFlashcardDeck(Guid flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto)
        {
            var flashcardDeck = await _flashcardDeckRepository.GetFlashcardDeckById(flashcardDeckId);

            if (flashcardDeck == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard deck not found");
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

        public async Task DeleteFlashcardDeck(Guid flashcardDeckId)
        {
            var flashcardDeck = await _flashcardDeckRepository.GetFlashcardDeckById(flashcardDeckId);

            if (flashcardDeck == null) 
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Flashcard deck not found");
            }

            await _flashcardDeckRepository.DeleteFlashcardDeck(flashcardDeck);
        }
    }
}
