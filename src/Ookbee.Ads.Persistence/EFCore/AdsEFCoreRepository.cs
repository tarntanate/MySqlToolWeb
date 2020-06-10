using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;

namespace Ookbee.Ads.Persistence.EFCore
{
    public class AdsEFCoreRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public AdsEFCoreRepository(AdsEFCoreContext context) : base(context)
        {

        }
    }
}
