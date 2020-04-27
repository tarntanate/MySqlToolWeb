using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;

namespace Ookbee.Ads.Persistence.Advertising.EntityFrameworkCore
{
    public class OokbeeAdsEfRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public OokbeeAdsEfRepository(OokbeeAdsEfContext context) : base(context)
        {

        }
    }
}
