using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQueryHandler : IRequestHandler<IsExistsAdByNameQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public IsExistsAdByNameQueryHandler(AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );
            
            if (!isExists)
                return result.Fail(404, $"Ad '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
