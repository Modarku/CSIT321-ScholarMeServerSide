using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestTest.Models
{
    public class Flashcard
    {
        [Key]
        public int FlashcardId { get; set; }

        [Required]
        [ForeignKey(nameof(UserAccount))]
        public int UserId { get; set; }
        public UserAccount UserAccount { get; set; }

        [Required]
        public string Question { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        List<FlashcardChoice>? Choices { get; set; }
    }
}
