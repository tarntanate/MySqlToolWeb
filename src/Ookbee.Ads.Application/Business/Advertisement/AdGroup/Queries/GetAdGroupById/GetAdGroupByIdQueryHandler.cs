using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupById
{
    public class GetAdGroupByIdQueryHandler : IRequestHandler<GetAdGroupByIdQuery, HttpResult<AdGroupDto>>
    {
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public GetAdGroupByIdQueryHandler(AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<AdGroupDto>> Handle(GetAdGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdGroupDbRepo.FirstAsync(
                selector: AdGroupDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new HttpResult<AdGroupDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdGroup '{request.Id}' doesn't exist.");
        }
    }
}
