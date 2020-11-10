using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQueryHandler : IRequestHandler<GetAdUnitByIdQuery, Response<AdUnitDto>>
    {
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public GetAdUnitByIdQueryHandler(
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<AdUnitDto>> Handle(GetAdUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdUnitEntity>();
            predicate = predicate.And(f => f.Id == request.Id);
            predicate = predicate.And(f => f.DeletedAt == null);
                
            var item = await AdUnitDbRepo.FirstAsync(
                include: f => 
                    f.Include(x => x.AdGroup.Publisher)
                     .Include(x => x.AdGroup.AdGroupType),
                selector: AdUnitDto.Projection,
                filter: predicate
            );

            var result = new Response<AdUnitDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
