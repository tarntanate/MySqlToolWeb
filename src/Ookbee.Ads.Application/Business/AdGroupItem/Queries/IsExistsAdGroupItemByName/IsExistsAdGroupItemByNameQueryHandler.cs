using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.IsExistsAdGroupItemByName
{
    public class IsExistsAdGroupItemByNameQueryHandler : IRequestHandler<IsExistsAdGroupItemByNameQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }

        public IsExistsAdGroupItemByNameQueryHandler(AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo)
        {
            AdGroupItemDbRepo = adGroupItemDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdGroupItemByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdGroupItemByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdGroupItemDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdGroupItem '{request.Name}' doesn't exist.");
        }
    }
}
