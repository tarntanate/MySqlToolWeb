using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitByName
{
    public class IsExistsAdUnitByNameQueryHandler : IRequestHandler<IsExistsAdUnitByNameQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<AdUnitEntity> AdUnitEFCoreRepo { get; }

        public IsExistsAdUnitByNameQueryHandler(AdsEFCoreRepository<AdUnitEntity> adUnitEFCoreRepo)
        {
            AdUnitEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdUnitByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdUnitByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdUnitEFCoreRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );
            
            if (!isExists)
                return result.Fail(404, $"AdUnit '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
