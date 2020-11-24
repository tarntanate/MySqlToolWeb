using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;
using System;

namespace Ookbee.Ads.Persistence.EFCore.TimeScaleDb.Configurations
{
    public class AdClickLogConfiguration : IEntityTypeConfiguration<AdClickLogEntity>
    {
        public void Configure(EntityTypeBuilder<AdClickLogEntity> builder)
        {
            builder.HasKey(e => e.CreatedAt);

            builder.Property(e => e.PlatformId);
        }
    }
}
