using RestTest.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestTest.DTO
{
    public class FlashcardSetDTO
    {

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
    }
}
