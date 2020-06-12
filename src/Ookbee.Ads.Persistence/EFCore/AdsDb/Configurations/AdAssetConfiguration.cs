using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdAssetConfiguration : IEntityTypeConfiguration<AdAssetEntity>
    {
        public void Configure(EntityTypeBuilder<AdAssetEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Ad)
                   .WithMany(e => e.AdAsset)
                   .HasForeignKey(e => e.AdId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
