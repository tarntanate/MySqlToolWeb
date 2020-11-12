using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdByName
{
    public class GetAdByNameQueryHandler : IRequestHandler<GetAdByNameQuery, Response<AdDto>>
    {
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public GetAdByNameQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<AdDto>> Handle(GetAdByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await AdDbRepo.FirstAsync<AdDto>(
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            var result = new Response<AdDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
