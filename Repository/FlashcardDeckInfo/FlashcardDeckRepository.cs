﻿using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;

namespace ScholarMeServer.Repository.FlashcardDeckInfo
{
    public class FlashcardDeckRepository : BaseRepository, IFlashcardDeckRepository
    {
        public FlashcardDeckRepository(ScholarMeDbContext scholarmeDbContext) : base(scholarmeDbContext) { }

        public async Task AddFlashcardDeck(FlashcardDeck flashcardDeck)
        {
            _scholarmeDbContext.Set<FlashcardDeck>().Add(flashcardDeck);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task<List<FlashcardDeck>> GetFlashcardDecksByUserId(Guid userAccountId)
        {
            var flashcardDecks = await _scholarmeDbContext.Set<FlashcardDeck>().Where(deck => deck.UserAccountId == userAccountId).ToListAsync();
            return flashcardDecks;
        }

        public async Task<FlashcardDeck?> GetFlashcardDeckById(Guid flashcardDeckId, bool includeFlashcards = false)
        {
            IQueryable<FlashcardDeck> query = _scholarmeDbContext.Set<FlashcardDeck>();

            if (includeFlashcards)
            {
                query = query.Include(deck => deck.Flashcards);
            }

            var flashcardDeck = await query.FirstOrDefaultAsync(deck => deck.Id == flashcardDeckId);
            return flashcardDeck;
        }

        public async Task SaveFlashcardDeck(FlashcardDeck flashcardDeck)
        {
            _scholarmeDbContext.Set<FlashcardDeck>().Update(flashcardDeck);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task DeleteFlashcardDeck(FlashcardDeck flashcardDeck)
        {
            _scholarmeDbContext.Set<FlashcardDeck>().Remove(flashcardDeck);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task<List<FlashcardDeck>> GetAllFlashcardDecks(bool includeFlashcards = false)
        {
            IQueryable<FlashcardDeck> query = _scholarmeDbContext.Set<FlashcardDeck>();

            if (includeFlashcards)
            {
                query = query.Include(deck => deck.Flashcards);
            }

            return await query.ToListAsync();
        }
    }
}
