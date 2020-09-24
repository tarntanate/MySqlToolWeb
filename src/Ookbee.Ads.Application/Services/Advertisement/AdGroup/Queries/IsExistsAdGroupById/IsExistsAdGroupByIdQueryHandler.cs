using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById
{
    public class IsExistsAdGroupByIdQueryHandler : IRequestHandler<IsExistsAdGroupByIdQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public IsExistsAdGroupByIdQueryHandler(
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdGroupDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
