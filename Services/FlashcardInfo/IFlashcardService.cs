﻿using ScholarMeServer.DTO.Flashcard;

namespace ScholarMeServer.Services.FlashcardInfo
{
    public interface IFlashcardService
    {
        public Task<FlashcardReadOnlyDto> CreateFlashcard(Guid flashcardDeckId, FlashcardCreateDto flashcardDto);
        public Task<List<FlashcardReadOnlyDto>> GetFlashcardsByDeckId(Guid flashcardDeckId, bool choices);
        public Task<FlashcardReadOnlyDto> GetFlashcardById(Guid flashcardId);
        public Task<FlashcardReadOnlyDto> UpdateFlashcard(Guid flashcardId, FlashcardUpdateDto flashcardDto);
        public Task DeleteFlashcard(Guid flashcardId);
    }
}
