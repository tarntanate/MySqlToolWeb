using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeById
{
    public class GetAdUnitTypeByIdQueryHandler : IRequestHandler<GetAdUnitTypeByIdQuery, HttpResult<AdUnitTypeDto>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public GetAdUnitTypeByIdQueryHandler(AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<HttpResult<AdUnitTypeDto>> Handle(GetAdUnitTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdUnitTypeDto>> GetOnDb(GetAdUnitTypeByIdQuery request)
        {
            var result = new HttpResult<AdUnitTypeDto>();

            var item = await AdUnitTypeDbRepo.FirstAsync(filter: f => f.Id == request.Id && f.DeletedAt == null);
            if (item == null)
                return result.Fail(404, $"AdUnitType '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<AdUnitTypeDto>();

            return result.Success(data);
        }
    }
}
