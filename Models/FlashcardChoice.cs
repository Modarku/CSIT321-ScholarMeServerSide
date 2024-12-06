using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestTest.Models
{
    public class FlashcardChoice
    {
        [Key]
        public int FlashcardChoiceId { get; set; }

        [ForeignKey(nameof(FlashcardId))]
        public int FlashcardId { get; set; }
        public Flashcard Flashcard { get; set; }

        [Required]
        public string Choice { get; set; }

        [Required]
        public bool IsAnswer { get; set; } = false;

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
