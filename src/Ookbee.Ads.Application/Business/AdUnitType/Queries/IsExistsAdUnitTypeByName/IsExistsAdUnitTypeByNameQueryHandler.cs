using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeByName
{
    public class IsExistsAdUnitTypeByNameQueryHandler : IRequestHandler<IsExistsAdUnitTypeByNameQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public IsExistsAdUnitTypeByNameQueryHandler(AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdUnitTypeByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdUnitTypeByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdUnitTypeDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"AdUnitType '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
