using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdConfiguration : IEntityTypeConfiguration<AdEntity>
    {
        public void Configure(EntityTypeBuilder<AdEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.AdUnit)
                   .WithMany(e => e.Ads)
                   .HasForeignKey(e => e.AdUnitId)
                   .IsRequired();

            builder.HasOne(e => e.Campaign)
                   .WithMany(e => e.Ads)
                   .HasForeignKey(e => e.CampaignId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Status)
                   .HasConversion(
                        v => v.ToString(),
                        v => (AdStatusType)Enum.Parse(typeof(AdStatusType), v));

            builder.Property(e => e.Platforms)
                   .HasConversion(
                        v => v.ToList().ConvertAll(x => x.ToString()),
                        v => v.Select(x => (AdPlatform)Enum.Parse(typeof(AdPlatform), x)).ToList());

            builder.Property(e => e.Platforms)
                   .Metadata
                   .SetValueComparer(new ValueComparer<List<AdPlatform>>(
                     (c1, c2) => c1.Equals(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()));
        }
    }
}
