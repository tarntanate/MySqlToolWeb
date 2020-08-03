using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemByName
{
    public class GetAdGroupItemByNameQueryHandler : IRequestHandler<GetAdGroupItemByNameQuery, HttpResult<AdGroupItemDto>>
    {
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }

        public GetAdGroupItemByNameQueryHandler(AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo)
        {
            AdGroupItemDbRepo = adGroupItemDbRepo;
        }

        public async Task<HttpResult<AdGroupItemDto>> Handle(GetAdGroupItemByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdGroupItemDto>> GetOnDb(GetAdGroupItemByNameQuery request)
        {
            var result = new HttpResult<AdGroupItemDto>();

            var item = await AdGroupItemDbRepo.FirstAsync(
                selector: AdGroupItemDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdGroupItem '{request.Name}' doesn't exist.");
        }
    }
}
