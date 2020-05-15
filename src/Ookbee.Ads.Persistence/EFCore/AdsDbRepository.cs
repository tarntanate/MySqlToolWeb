using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;

namespace Ookbee.Ads.Persistence.EFCore
{
    public class OokbeeAdsEFCoreRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public OokbeeAdsEFCoreRepository(OokbeeAdsEFCoreContext context) : base(context)
        {

        }
    }
}
