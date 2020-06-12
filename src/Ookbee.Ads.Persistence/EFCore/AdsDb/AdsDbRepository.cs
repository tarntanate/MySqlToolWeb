using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;

namespace Ookbee.Ads.Persistence.EFCore.AdsDb
{
    public class AdsDbRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public AdsDbRepository(AdsDbContext context) : base(context)
        {

        }
    }
}
