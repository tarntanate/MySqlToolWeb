using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById
{
    public class GetAdvertiserByIdQueryHandler : IRequestHandler<GetAdvertiserByIdQuery, HttpResult<AdvertiserDto>>
    {
        private AdsEFCoreRepository<AdvertiserEntity> AdvertiserEFCoreRepo { get; }

        public GetAdvertiserByIdQueryHandler(AdsEFCoreRepository<AdvertiserEntity> advertiserEFCoreRepo)
        {
            AdvertiserEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<AdvertiserDto>> Handle(GetAdvertiserByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdvertiserDto>> GetOnDb(GetAdvertiserByIdQuery request)
        {
            var result = new HttpResult<AdvertiserDto>();

            var item = await AdvertiserEFCoreRepo.FirstAsync(filter: f =>
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
