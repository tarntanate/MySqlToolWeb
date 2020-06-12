using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdUnitTypeConfiguration : IEntityTypeConfiguration<AdUnitTypeEntity>
    {
        public void Configure(EntityTypeBuilder<AdUnitTypeEntity> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
