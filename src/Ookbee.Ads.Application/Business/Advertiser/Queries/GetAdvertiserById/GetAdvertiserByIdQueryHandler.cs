using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById
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
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdvertiserDto>> GetOnDb(GetAdvertiserByIdQuery request)
        {
            var result = new HttpResult<AdvertiserDto>();

            var item = await AdvertiserDbRepo.FirstAsync(filter: f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );
            
            if (item == null)
                return result.Fail(404, $"Advertiser '{request.Id}' doesn't exist.");

            var data = Mapper.Map(item).ToANew<AdvertiserDto>();
            return result.Success(data);
        }
    }
}
