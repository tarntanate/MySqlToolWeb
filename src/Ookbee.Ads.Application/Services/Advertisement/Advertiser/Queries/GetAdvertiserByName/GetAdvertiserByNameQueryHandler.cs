using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.GetAdvertiserByName
{
    public class GetAdvertiserByNameQueryHandler : IRequestHandler<GetAdvertiserByNameQuery, Response<AdvertiserDto>>
    {
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public GetAdvertiserByNameQueryHandler(
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<Response<AdvertiserDto>> Handle(GetAdvertiserByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await AdvertiserDbRepo.FirstAsync(
                selector: AdvertiserDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            var result = new Response<AdvertiserDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Data not found.");
        }
    }
}
