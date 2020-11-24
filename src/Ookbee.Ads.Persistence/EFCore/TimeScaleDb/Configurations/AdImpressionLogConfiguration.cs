using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;
using System;

namespace Ookbee.Ads.Persistence.EFCore.TimeScaleDb.Configurations
{
    public class AdImpressionLogConfiguration : IEntityTypeConfiguration<AdImpressionLogEntity>
    {
        public void Configure(EntityTypeBuilder<AdImpressionLogEntity> builder)
        {
            builder.HasKey(e => e.CreatedAt);

            builder.Property(e => e.PlatformId);
        }
    }
}
