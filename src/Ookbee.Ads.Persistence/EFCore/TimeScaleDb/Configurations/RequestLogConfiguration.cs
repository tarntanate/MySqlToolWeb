using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using System;

namespace Ookbee.Ads.Persistence.EFCore.TimeScaleDb.Configurations
{
    public class TimeScaleDbConfiguration : IEntityTypeConfiguration<RequestLogEntity>
    {
        public void Configure(EntityTypeBuilder<RequestLogEntity> builder)
        {
            builder.HasKey(e => e.CreatedAt);

            builder.Property(e => e.PlatformId);
        }
    }
}
