using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeById
{
    public class IsExistsAdUnitTypeByIdQueryHandler : IRequestHandler<IsExistsAdUnitTypeByIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<AdUnitTypeEntity> AdUnitTypeEFCoreRepo { get; }

        public IsExistsAdUnitTypeByIdQueryHandler(AdsEFCoreRepository<AdUnitTypeEntity> adUnitTypeEFCoreRepo)
        {
            AdUnitTypeEFCoreRepo = adUnitTypeEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdUnitTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdUnitTypeByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdUnitTypeEFCoreRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );
            
            if (!isExists)
                return result.Fail(404, $"AdUnitType '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
