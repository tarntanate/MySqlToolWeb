using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdById
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
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdDto>> GetOnDb(GetAdByIdQuery request)
        {
            var result = new HttpResult<AdDto>();

            var item = await AdDbRepo.FirstAsync(filter: f => f.Id == request.Id && f.DeletedAt == null);
            if (item == null)
                return result.Fail(404, $"Ad '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<AdDto>();

            return result.Success(data);
        }
    }
}
