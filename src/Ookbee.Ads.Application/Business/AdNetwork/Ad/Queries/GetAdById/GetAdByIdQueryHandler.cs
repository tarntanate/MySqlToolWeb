using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdById
{
    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, HttpResult<AdDto>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public GetAdByIdQueryHandler(AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<AdDto>> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<AdDto>();

            var item = await AdDbRepo.FirstAsync(
                selector: AdDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null);

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Ad '{request.Id}' doesn't exist.");
        }
    }
}
