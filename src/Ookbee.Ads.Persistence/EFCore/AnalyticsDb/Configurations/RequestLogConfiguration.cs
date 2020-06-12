using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Persistence.EFCore.AnalyticsDb.Configurations
{
    public class RequestLogConfiguration : IEntityTypeConfiguration<RequestLogEntity>
    {
        public void Configure(EntityTypeBuilder<RequestLogEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
