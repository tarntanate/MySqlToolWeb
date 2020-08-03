using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQueryHandler : IRequestHandler<IsExistsAdGroupByNameQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public IsExistsAdGroupByNameQueryHandler(AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdGroupByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdGroupByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdGroupDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"AdGroup '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
