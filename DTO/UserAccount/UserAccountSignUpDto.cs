using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.UserAccount
{
    // Registration Fields
    public class UserAccountSignUpDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        // TODO: Philippines phone number validation specific
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
