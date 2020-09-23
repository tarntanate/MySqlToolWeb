using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeByName
{
    public class IsExistsAdUnitTypeByNameQueryHandler : IRequestHandler<IsExistsAdUnitTypeByNameQuery, Response<bool>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public IsExistsAdUnitTypeByNameQueryHandler(AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdUnitTypeByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitTypeDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            if (!isExists)
                return result.Fail(404, $"AdUnitType '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
