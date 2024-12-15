﻿using System.Text.Json.Serialization;

namespace ScholarMeServer.DTO.FlashcardChoice
{
    public class FlashcardChoiceReadOnlyDto
    {
        public Guid Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? FlashcardId { get; set; }

        public string Choice { get; set; }

        public bool IsAnswer { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
