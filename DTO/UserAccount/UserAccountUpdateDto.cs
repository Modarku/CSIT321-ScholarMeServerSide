using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.UserAccount
{
    public class UserAccountUpdateDto
    {
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        // TODO: Philippines phone number validation specific
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
