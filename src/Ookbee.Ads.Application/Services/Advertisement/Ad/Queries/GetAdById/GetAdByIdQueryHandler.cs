using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById
{
    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, Response<AdDto>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public GetAdByIdQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<AdDto>> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdDbRepo.FirstAsync(
                selector: AdDto.Projection,
                filter: f => 
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<AdDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Data not found.");
        }
    }
}
