﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Enums;
using System;
using System.Linq;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdUnitConfiguration : IEntityTypeConfiguration<AdUnitEntity>
    {
        public void Configure(EntityTypeBuilder<AdUnitEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.AdUnitType)
                   .WithMany(e => e.AdUnits)
                   .HasForeignKey(e => e.AdUnitTypeId)
                   .IsRequired();

            builder.HasOne(e => e.Publisher)
                   .WithMany(e => e.AdUnits)
                   .HasForeignKey(e => e.PublisherId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
