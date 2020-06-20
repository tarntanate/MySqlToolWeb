using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById
{
    public class IsExistsAdUnitByIdQueryHandler : IRequestHandler<IsExistsAdUnitByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public IsExistsAdUnitByIdQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdUnitByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdUnitByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdUnitDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdUnit '{request.Id}' doesn't exist.");
        }
    }
}
