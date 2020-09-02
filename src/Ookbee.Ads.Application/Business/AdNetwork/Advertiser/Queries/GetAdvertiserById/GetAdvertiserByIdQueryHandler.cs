using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Queries.GetAdvertiserById
{
    public class GetAdvertiserByIdQueryHandler : IRequestHandler<GetAdvertiserByIdQuery, HttpResult<AdvertiserDto>>
    {
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public GetAdvertiserByIdQueryHandler(AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<AdvertiserDto>> Handle(GetAdvertiserByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdvertiserDbRepo.FirstAsync(
                selector: AdvertiserDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new HttpResult<AdvertiserDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Advertiser '{request.Id}' doesn't exist.");
        }
    }
}
