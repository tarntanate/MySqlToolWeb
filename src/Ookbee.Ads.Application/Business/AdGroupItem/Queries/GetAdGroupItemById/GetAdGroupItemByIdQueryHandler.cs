using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemById
{
    public class GetAdGroupItemByIdQueryHandler : IRequestHandler<GetAdGroupItemByIdQuery, HttpResult<AdGroupItemDto>>
    {
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }

        public GetAdGroupItemByIdQueryHandler(AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo)
        {
            AdGroupItemDbRepo = adGroupItemDbRepo;
        }

        public async Task<HttpResult<AdGroupItemDto>> Handle(GetAdGroupItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdGroupItemDto>> GetOnDb(GetAdGroupItemByIdQuery request)
        {
            var result = new HttpResult<AdGroupItemDto>();

            var item = await AdGroupItemDbRepo.FirstAsync(
                selector: AdGroupItemDto.Projection,
                filter: f => f.Id == request.Id && f.DeletedAt == null
            );

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdGroupItem '{request.Id}' doesn't exist.");
        }
    }
}
