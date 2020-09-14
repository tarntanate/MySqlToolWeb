using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLogEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityLogEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.User)
                   .WithMany(e => e.ActivityLogs)
                   .HasForeignKey(e => e.UserId)
                   .IsRequired();

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Activity)
                   .HasConversion(
                        v => v.ToString(),
                        v => (LogEvent)Enum.Parse(typeof(LogEvent), v));
        }
    }
}
