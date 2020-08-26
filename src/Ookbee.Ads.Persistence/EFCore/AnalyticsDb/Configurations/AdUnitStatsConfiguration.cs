using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Persistence.EFCore.AnalyticsDb.Configurations
{
    public class AdUnitStatsConfiguration : IEntityTypeConfiguration<AdUnitStatsEntity>
    {
        public void Configure(EntityTypeBuilder<AdUnitStatsEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Platform)
                   .HasConversion(
                        v => v.ToString(),
                        v => (Platform)Enum.Parse(typeof(Platform), v));

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
