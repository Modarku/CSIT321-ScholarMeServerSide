namespace ScholarMeServer.DTO.RefreshTokenRequest
{
    public class RefreshTokenReadOnly
    {
        public Guid Id { get; set; }

        public int UserAccountId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresOnUtc { get; set; }
    }
}
