using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataShareHub.Seeds
{
    public class IdentityRoleConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = ADMINROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = USERROLE_ID,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }
    }
}
