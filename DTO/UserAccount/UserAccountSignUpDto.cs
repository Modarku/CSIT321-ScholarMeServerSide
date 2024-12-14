using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.UserAccount
{
    // Registration Fields
    public class UserAccountSignUpDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 characters.")]
        [MaxLength(50, ErrorMessage = "Username must not exceed 50 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 characters.")]
        [MaxLength(255, ErrorMessage = "Password must not exceed 255 characters.")]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string LastName { get; set; }

        // TODO: Philippines phone number validation specific
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
