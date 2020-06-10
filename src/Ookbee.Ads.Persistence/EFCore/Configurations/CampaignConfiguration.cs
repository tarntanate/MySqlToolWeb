using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Persistence.EFCore.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<CampaignEntity>
    {
        public void Configure(EntityTypeBuilder<CampaignEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Advertiser)
                   .WithMany(e => e.Campaigns)
                   .HasForeignKey(e => e.AdvertiserId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
