using System.ComponentModel.DataAnnotations;

namespace ScholarMeServer.DTO.RefreshToken
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
