using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
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
                        v => (AdStatus)Enum.Parse(typeof(AdStatus), v));

            builder.Property(e => e.Platforms)
                   .HasConversion(
                        v => v.ConvertAll(x => x.ToString()),
                        v => v.Select(x => (Platform)Enum.Parse(typeof(Platform), x)).ToList());
        }
    }
}
