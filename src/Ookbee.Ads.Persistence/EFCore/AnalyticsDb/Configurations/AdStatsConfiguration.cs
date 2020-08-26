using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Persistence.EFCore.AnalyticsDb.Configurations
{
    public class AdStatsConfiguration : IEntityTypeConfiguration<AdStatsEntity>
    {
        public void Configure(EntityTypeBuilder<AdStatsEntity> builder)
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
