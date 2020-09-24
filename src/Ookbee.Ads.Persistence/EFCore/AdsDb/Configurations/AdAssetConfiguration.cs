﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdAssetConfiguration : IEntityTypeConfiguration<AdAssetEntity>
    {
        public void Configure(EntityTypeBuilder<AdAssetEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Ad)
                   .WithMany(e => e.AdAssets)
                   .HasForeignKey(e => e.AdId);

            builder.Property(e => e.Position)
                   .HasConversion(
                        v => v.ToString(),
                        v => (AdPosition)Enum.Parse(typeof(AdPosition), v));

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
