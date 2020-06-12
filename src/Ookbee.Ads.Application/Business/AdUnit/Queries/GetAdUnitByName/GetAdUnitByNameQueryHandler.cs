using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitByName
{
    public class GetAdUnitByNameQueryHandler : IRequestHandler<GetAdUnitByNameQuery, HttpResult<AdUnitDto>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public GetAdUnitByNameQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo )
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<AdUnitDto>> Handle(GetAdUnitByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdUnitDto>> GetOnDb(GetAdUnitByNameQuery request)
        {
            var result = new HttpResult<AdUnitDto>();

            var item = await AdUnitDbRepo.FirstAsync(filter: f => f.Name == request.Name && f.DeletedAt == null);
            if (item == null)
                return result.Fail(404, $"AdUnit '{request.Name}' doesn't exist.");

            var data = Mapper
                .Map(item)
                .ToANew<AdUnitDto>();

            return result.Success(data);
        }
    }
}
