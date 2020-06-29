using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserByName
{
    public class GetAdvertiserByNameQueryHandler : IRequestHandler<GetAdvertiserByNameQuery, HttpResult<AdvertiserDto>>
    {
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public GetAdvertiserByNameQueryHandler(AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<AdvertiserDto>> Handle(GetAdvertiserByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdvertiserDto>> GetOnDb(GetAdvertiserByNameQuery request)
        {
            var result = new HttpResult<AdvertiserDto>();

            var item = await AdvertiserDbRepo.FirstAsync(
                selector: AdvertiserDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Advertiser '{request.Name}' doesn't exist.");
        }
    }
}
