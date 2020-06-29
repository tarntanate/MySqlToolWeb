using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AuthPermissionConfiguration : IEntityTypeConfiguration<UserPermissionEntity>
    {
        public void Configure(EntityTypeBuilder<UserPermissionEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Role)
                   .WithMany(e => e.Permissions)
                   .HasForeignKey(e => e.AuthRoleId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
