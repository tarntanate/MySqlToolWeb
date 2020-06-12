using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Persistence.EFCore.AdDb.Configurations
{
    public class AdvertiserConfiguration : IEntityTypeConfiguration<AdvertiserEntity>
    {
        public void Configure(EntityTypeBuilder<AdvertiserEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
