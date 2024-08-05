using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TwseDataHub.Seeds
{
    public class IdentityUserRoleConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMINROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = USERROLE_ID,
                    UserId = ADMIN_ID
                }
            );
        }
    }
}
