using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.UserAccount
{
    public class UserAccountChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 characters.")]
        [MaxLength(255, ErrorMessage = "Password must not exceed 255 characters.")]
        public string NewPassword { get; set; }
    }
}
