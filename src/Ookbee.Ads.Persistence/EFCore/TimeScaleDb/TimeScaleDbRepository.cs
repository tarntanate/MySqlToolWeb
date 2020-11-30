using AutoMapper;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;

namespace Ookbee.Ads.Persistence.EFCore.TimeScaleDb
{
    public class TimeScaleDbRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public TimeScaleDbRepository(IMapper mapper, TimescaleDbContext context) : base(mapper, context)
        {
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
    }
}