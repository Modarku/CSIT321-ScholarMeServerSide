using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.UserAccount
{
    public class UserAccountUpdateDto
    {
        [StringLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        [MinLength(1)]
        [MaxLength(255)]
        public string? FirstName { get; set; }

        [MinLength(1)]
        [MaxLength(255)]
        public string? LastName { get; set; }

        // TODO: Philippines phone number validation specific
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
