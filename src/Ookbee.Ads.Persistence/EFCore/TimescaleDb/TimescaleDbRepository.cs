using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;

namespace Ookbee.Ads.Persistence.EFCore.TimescaleDb
{
    public class TimescaleDbRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public TimescaleDbRepository(TimescaleDbContext context) : base(context)
        {

        }
    }
}
