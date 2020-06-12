using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities;

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
        }
    }
}
