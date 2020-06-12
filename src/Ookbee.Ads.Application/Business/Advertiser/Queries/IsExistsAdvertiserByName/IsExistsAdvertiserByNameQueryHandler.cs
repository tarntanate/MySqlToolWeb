using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName
{
    public class IsExistsAdvertiserByNameQueryHandler : IRequestHandler<IsExistsAdvertiserByNameQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public IsExistsAdvertiserByNameQueryHandler(AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdvertiserByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdvertiserByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdvertiserDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );
            
            if (!isExists)
                return result.Fail(404, $"Advertiser '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
