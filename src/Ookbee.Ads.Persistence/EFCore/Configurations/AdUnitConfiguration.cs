using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Persistence.EFCore.Configurations
{
    public class AdUnitConfiguration : IEntityTypeConfiguration<AdUnitEntity>
    {
        public void Configure(EntityTypeBuilder<AdUnitEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.AdUnitType)
                   .WithMany(e => e.AdUnits)
                   .HasForeignKey(e => e.AdUnitTypeId)
                   .IsRequired();
                   
            builder.HasOne(e => e.Publisher)
                   .WithMany(e => e.AdUnits)
                   .HasForeignKey(e => e.PublisherId)
                   .IsRequired();

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
