using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;

namespace Ookbee.Ads.Persistence.EFCore.TimeScaleDb
{
    public class TimeScaleDbRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public TimeScaleDbRepository(TimeScaleDbContext context) : base(context)
        {

        }
    }
}
