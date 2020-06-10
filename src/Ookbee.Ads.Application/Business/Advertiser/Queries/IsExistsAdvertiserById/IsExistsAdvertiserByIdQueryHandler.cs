using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQueryHandler : IRequestHandler<IsExistsAdvertiserByIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<AdvertiserEntity> AdvertiserEFCoreRepo { get; }

        public IsExistsAdvertiserByIdQueryHandler(AdsEFCoreRepository<AdvertiserEntity> advertiserEFCoreRepo)
        {
            AdvertiserEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdvertiserByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdvertiserByIdQuery request)
        {
            var result = new HttpResult<bool>();
            
            var isExists = await AdvertiserEFCoreRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );
            
            if (!isExists)
                return result.Fail(404, $"Advertiser '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
