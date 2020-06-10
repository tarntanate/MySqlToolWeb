using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById
{
    public class IsExistsAdByIdQueryHandler : IRequestHandler<IsExistsAdByIdQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<AdEntity> AdEFCoreRepo { get; }

        public IsExistsAdByIdQueryHandler(AdsEFCoreRepository<AdEntity> adEFCoreRepo)
        {
            AdEFCoreRepo = adEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdEFCoreRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );
            
            if (!isExists)
                return result.Fail(404, $"Ad '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
