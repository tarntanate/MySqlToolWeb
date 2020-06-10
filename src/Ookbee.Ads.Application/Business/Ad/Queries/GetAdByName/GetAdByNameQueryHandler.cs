using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByName
{
    public class GetAdByNameQueryHandler : IRequestHandler<GetAdByNameQuery, HttpResult<AdDto>>
    {
        private AdsEFCoreRepository<AdEntity> AdEFCoreRepo { get; }

        public GetAdByNameQueryHandler(AdsEFCoreRepository<AdEntity> adEFCoreRepo)
        {
            AdEFCoreRepo = adEFCoreRepo;
        }

        public async Task<HttpResult<AdDto>> Handle(GetAdByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdDto>> GetOnDb(GetAdByNameQuery request)
        {
            var result = new HttpResult<AdDto>();

            var item = await AdEFCoreRepo.FirstAsync(filter: f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            if (item == null)
                return result.Fail(404, $"Ad '{request.Name}' doesn't exist.");

            var data = Mapper
                .Map(item)
                .ToANew<AdDto>();

            return result.Success(data);
        }
    }
}
