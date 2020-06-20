using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQueryHandler : IRequestHandler<IsExistsAdvertiserByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public IsExistsAdvertiserByIdQueryHandler(AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdvertiserByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdvertiserByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdvertiserDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Advertiser '{request.Id}' doesn't exist.");
        }
    }
}
