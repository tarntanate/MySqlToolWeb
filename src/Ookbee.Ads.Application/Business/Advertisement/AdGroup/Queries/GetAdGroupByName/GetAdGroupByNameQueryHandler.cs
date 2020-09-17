using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupByName
{
    public class GetAdGroupByNameQueryHandler : IRequestHandler<GetAdGroupByNameQuery, HttpResult<AdGroupDto>>
    {
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public GetAdGroupByNameQueryHandler(AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<AdGroupDto>> Handle(GetAdGroupByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await AdGroupDbRepo.FirstAsync(
                selector: AdGroupDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null);

            var result = new HttpResult<AdGroupDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdGroup '{request.Name}' doesn't exist.");
        }
    }
}
