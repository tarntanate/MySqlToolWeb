using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.IsExistsAdGroupItemById
{
    public class IsExistsAdGroupItemByIdQueryHandler : IRequestHandler<IsExistsAdGroupItemByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }

        public IsExistsAdGroupItemByIdQueryHandler(AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo)
        {
            AdGroupItemDbRepo = adGroupItemDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdGroupItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdGroupItemByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdGroupItemDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdGroupItem '{request.Id}' doesn't exist.");
        }
    }
}
