using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByName
{
    public class GetAdByNameQueryHandler : IRequestHandler<GetAdByNameQuery, HttpResult<AdDto>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public GetAdByNameQueryHandler(AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<AdDto>> Handle(GetAdByNameQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<AdDto>();

            var item = await AdDbRepo.FirstAsync(
                selector: AdDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"'Ad' doesn't exist.");
        }
    }
}
