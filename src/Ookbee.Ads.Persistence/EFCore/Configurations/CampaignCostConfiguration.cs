using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Persistence.EFCore.Configurations
{
    public class CampaignCostPerUnitConfiguration : IEntityTypeConfiguration<CampaignCostEntity>
    {
        public void Configure(EntityTypeBuilder<CampaignCostEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Campaign)
                   .WithOne(e => e.CampaignCost)
                   .HasForeignKey<CampaignCostEntity>(e => e.CampaignId);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
