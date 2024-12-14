using Microsoft.EntityFrameworkCore;

namespace ScholarMeServer.Models.Configurations
{
    public class RefreshTokenVerification : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.Token).HasMaxLength(100);
            // Index on token to ensure uniqueness and faster query lookup
            builder.HasIndex(rt => rt.Token).IsUnique();
            builder.HasOne(rt => rt.UserAccount).WithMany().HasForeignKey(rt => rt.UserAccountId);
        }
    }
}
