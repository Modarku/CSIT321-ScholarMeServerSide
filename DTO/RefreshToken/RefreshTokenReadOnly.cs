namespace ScholarMeServer.DTO.RefreshTokenRequest
{
    public class RefreshTokenReadOnly
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresOnUtc { get; set; }
    }
}
