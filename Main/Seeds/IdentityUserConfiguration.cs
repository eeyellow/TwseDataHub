using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataShareHub.Seeds
{
    public class IdentityUserConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            builder.HasData(
                new IdentityUser
                {
                    Id = ADMIN_ID,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@your.company.tw",
                    NormalizedEmail = "ADMIN@YOUR.COMPANY.TW",
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(null, "a123456"),
                    SecurityStamp = string.Empty
                }
            );
        }
    }
}
