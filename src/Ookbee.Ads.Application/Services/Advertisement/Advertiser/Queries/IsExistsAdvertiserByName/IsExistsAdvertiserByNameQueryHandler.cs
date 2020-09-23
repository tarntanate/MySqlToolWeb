using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.IsExistsAdvertiserByName
{
    public class IsExistsAdvertiserByNameQueryHandler : IRequestHandler<IsExistsAdvertiserByNameQuery, Response<bool>>
    {
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public IsExistsAdvertiserByNameQueryHandler(AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdvertiserByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdvertiserDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Advertiser '{request.Name}' doesn't exist.");
        }
    }
}
