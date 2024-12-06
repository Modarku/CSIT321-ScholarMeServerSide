using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestTest.Models
{
    public class FlashcardSet
    {
        [Key]
        public int FlashcardSetId { get; set; }

        [ForeignKey(nameof(UserAccount))]
        public int UserId { get; set; }
        public UserAccount UserAccount { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public List<Flashcard>? Flashcards { get; set; }
    }
}
