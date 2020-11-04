using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdGroupConfiguration : IEntityTypeConfiguration<AdGroupEntity>
    {
        public void Configure(EntityTypeBuilder<AdGroupEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.AdGroupType)
                   .WithMany(e => e.AdGroups)
                   .HasForeignKey(e => e.AdGroupTypeId)
                   .IsRequired();

            builder.HasOne(e => e.Publisher)
                   .WithMany(e => e.AdGroups)
                   .HasForeignKey(e => e.PublisherId)
                   .IsRequired();

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
