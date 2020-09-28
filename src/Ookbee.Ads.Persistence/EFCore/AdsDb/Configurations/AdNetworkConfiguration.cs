using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdNetworkConfiguration : IEntityTypeConfiguration<AdNetworkEntity>
    {
        public void Configure(EntityTypeBuilder<AdNetworkEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Platform)
                   .HasConversion(
                        v => v.ToString(),
                        v => (AdPlatform)Enum.Parse(typeof(AdPlatform), v));

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
