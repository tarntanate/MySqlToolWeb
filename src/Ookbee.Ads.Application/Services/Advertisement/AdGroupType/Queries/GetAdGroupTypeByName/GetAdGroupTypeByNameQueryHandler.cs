using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeByName
{
    public class GetAdGroupTypeByNameQueryHandler : IRequestHandler<GetAdGroupTypeByNameQuery, Response<AdGroupTypeDto>>
    {
        private readonly AdsDbRepository<AdGroupTypeEntity> AdGroupTypeDbRepo;

        public GetAdGroupTypeByNameQueryHandler(
            AdsDbRepository<AdGroupTypeEntity> adGroupTypeDbRepo)
        {
            AdGroupTypeDbRepo = adGroupTypeDbRepo;
        }

        public async Task<Response<AdGroupTypeDto>> Handle(GetAdGroupTypeByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await AdGroupTypeDbRepo.FirstAsync<AdGroupTypeDto>(
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null);

            var result = new Response<AdGroupTypeDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
