using RestTest.Models;

namespace ScholarMeServer.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresOnUtc { get; set; }

        // Navigation Property
        public UserAccount UserAccount { get; set; }
    }
}
