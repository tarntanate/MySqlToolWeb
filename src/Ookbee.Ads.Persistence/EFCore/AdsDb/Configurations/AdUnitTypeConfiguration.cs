using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdGroupTypeConfiguration : IEntityTypeConfiguration<AdGroupTypeEntity>
    {
        public void Configure(EntityTypeBuilder<AdGroupTypeEntity> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
