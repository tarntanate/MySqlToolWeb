using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class CampaignImpressionConfiguration : IEntityTypeConfiguration<CampaignImpressionEntity>
    {
        public void Configure(EntityTypeBuilder<CampaignImpressionEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Campaign)
                   .WithOne(e => e.CampaignImpression)
                   .HasForeignKey<CampaignImpressionEntity>(e => e.CampaignId);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
