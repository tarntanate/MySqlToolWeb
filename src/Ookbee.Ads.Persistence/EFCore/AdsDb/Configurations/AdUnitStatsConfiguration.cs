using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AnalyticsDb.Configurations
{
    public class AdUnitStatsConfiguration : IEntityTypeConfiguration<AdUnitStatsEntity>
    {
        public void Configure(EntityTypeBuilder<AdUnitStatsEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
