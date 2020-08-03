using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdGroupItemConfiguration : IEntityTypeConfiguration<AdGroupItemEntity>
    {
        public void Configure(EntityTypeBuilder<AdGroupItemEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.AdGroup)
                   .WithMany(e => e.AdGroupItems)
                   .HasForeignKey(e => e.AdGroupId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
