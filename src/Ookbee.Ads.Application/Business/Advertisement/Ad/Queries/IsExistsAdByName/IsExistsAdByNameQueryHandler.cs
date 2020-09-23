using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQueryHandler : IRequestHandler<IsExistsAdByNameQuery, Response<bool>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public IsExistsAdByNameQueryHandler(AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Ad '{request.Name}' doesn't exist.");
        }
    }
}
