using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeById
{
    public class GetAdGroupTypeByIdQueryHandler : IRequestHandler<GetAdGroupTypeByIdQuery, Response<AdGroupTypeDto>>
    {
        private readonly AdsDbRepository<AdGroupTypeEntity> AdGroupTypeDbRepo;

        public GetAdGroupTypeByIdQueryHandler(
            AdsDbRepository<AdGroupTypeEntity> adGroupTypeDbRepo)
        {
            AdGroupTypeDbRepo = adGroupTypeDbRepo;
        }

        public async Task<Response<AdGroupTypeDto>> Handle(GetAdGroupTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdGroupTypeDbRepo.FirstAsync<AdGroupTypeDto>(
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<AdGroupTypeDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
