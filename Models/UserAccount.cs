using System.ComponentModel.DataAnnotations;

namespace RestTest.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public Models.UserRole Role { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(11)]
        public string? PhoneNumber { get; set; }

        [StringLength(255)]
        public string? ProfilePic { get; set; }

        [Required]
        public Models.UserStatus Status { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public List<FlashcardSet>? FlashcardSets { get; set; }

    }

    public enum UserRole
    {
        Student,
        Teacher
    }

    public enum UserStatus
    {
        Active,
        Inactive
    }

}
