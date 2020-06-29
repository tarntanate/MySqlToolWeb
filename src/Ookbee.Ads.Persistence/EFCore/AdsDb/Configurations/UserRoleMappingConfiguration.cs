using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class UserRoleMappingConfiguration : IEntityTypeConfiguration<UserRoleMappingEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleMappingEntity> builder)
        {
            builder.HasKey(e => new { e.UserId, e.RoleId });

            builder.HasOne(e => e.User)
                   .WithMany(b => b.UserRoleMappings)
                   .HasForeignKey(bc => bc.UserId);

            builder.HasOne(e => e.Role)
                   .WithMany(b => b.UserRoleMappings)
                   .HasForeignKey(bc => bc.RoleId);
        }
    }
}
