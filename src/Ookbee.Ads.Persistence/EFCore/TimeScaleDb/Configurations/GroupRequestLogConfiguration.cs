using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;
using System;

namespace Ookbee.Ads.Persistence.EFCore.TimeScaleDb.Configurations
{
    public class GroupRequestLogConfiguration : IEntityTypeConfiguration<GroupRequestLogEntity>
    {
        public void Configure(EntityTypeBuilder<GroupRequestLogEntity> builder)
        {
            builder.HasKey(e => e.CreatedAt);

            builder.Property(e => e.PlatformId);
        }
    }
}
