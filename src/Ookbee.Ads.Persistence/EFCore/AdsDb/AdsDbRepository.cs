using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Common.EntityFrameworkCore.Repository;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Enums;
using System.Threading.Tasks;

namespace Ookbee.Ads.Persistence.EFCore.AdsDb
{
    public class AdsDbRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public AdsDbRepository(AdsDbContext context) : base(context)
        {

        }
    }
}
