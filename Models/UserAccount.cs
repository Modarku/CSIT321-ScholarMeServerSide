﻿using ScholarMeServer.Models;
using System.ComponentModel.DataAnnotations;

// Model for individual user accounts
namespace RestTest.Models
{
    public class UserAccount
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public List<FlashcardDeck> FlashcardDecks { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
