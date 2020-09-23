using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupById
{
    public class GetAdGroupByIdQueryHandler : IRequestHandler<GetAdGroupByIdQuery, Response<AdGroupDto>>
    {
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public GetAdGroupByIdQueryHandler(AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<AdGroupDto>> Handle(GetAdGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdGroupDbRepo.FirstAsync(
                selector: AdGroupDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<AdGroupDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdGroup '{request.Id}' doesn't exist.");
        }
    }
}
