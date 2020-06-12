using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;

namespace Ookbee.Ads.Persistence.EFCore.AnalyticsDb
{
    public class AnalyticsDbRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public AnalyticsDbRepository(AnalyticsDbContext context) : base(context)
        {

        }
    }
}
