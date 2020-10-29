using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AnalyticsDb.Configurations
{
    public class AdGroupStatsConfiguration : IEntityTypeConfiguration<AdGroupStatsEntity>
    {
        public void Configure(EntityTypeBuilder<AdGroupStatsEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.AdGroup)
                   .WithMany(e => e.AdGroupStats)
                   .HasForeignKey(e => e.AdGroupId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
