using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.IsExistsAdGroupTypeByName
{
    public class IsExistsAdGroupTypeByNameQueryHandler : IRequestHandler<IsExistsAdGroupTypeByNameQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdGroupTypeEntity> AdGroupTypeDbRepo;

        public IsExistsAdGroupTypeByNameQueryHandler(
            AdsDbRepository<AdGroupTypeEntity> adGroupTypeDbRepo)
        {
            AdGroupTypeDbRepo = adGroupTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdGroupTypeByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdGroupTypeDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
