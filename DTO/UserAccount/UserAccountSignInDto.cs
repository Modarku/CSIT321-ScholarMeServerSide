using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.UserAccount
{
    // Login Fields
    public class UserAccountSignInDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 characters.")]
        [MaxLength(50, ErrorMessage = "Username must not exceed 50 characters.")]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 characters.")]
        [MaxLength(255, ErrorMessage = "Password must not exceed 255 characters.")]
        public string Password { get; set; }
    }
}
