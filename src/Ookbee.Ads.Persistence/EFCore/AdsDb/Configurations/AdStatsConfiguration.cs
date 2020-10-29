﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AnalyticsDb.Configurations
{
    public class AdStatsConfiguration : IEntityTypeConfiguration<AdStatsEntity>
    {
        public void Configure(EntityTypeBuilder<AdStatsEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Ad)
                   .WithMany(e => e.AdStats)
                   .HasForeignKey(e => e.AdId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}