﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.EFCore;

namespace Ookbee.Ads.Persistence.EFCore.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<CampaignEntity>
    {
        public void Configure(EntityTypeBuilder<CampaignEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}
