using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById
{
    public class IsExistsAdUnitByIdQueryHandler : IRequestHandler<IsExistsAdUnitByIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<AdUnitEntity> AdUnitEFCoreRepo { get; }

        public IsExistsAdUnitByIdQueryHandler(AdsEFCoreRepository<AdUnitEntity> adUnitEFCoreRepo)
        {
            AdUnitEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdUnitByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdUnitByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdUnitEFCoreRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"AdUnit '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
