using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Commands.UpdateAdGroupStats
{
    public class UpdateAdGroupStatsCommandHandler : IRequestHandler<UpdateAdGroupStatsCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public UpdateAdGroupStatsCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mapper = mapper;
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupStatsEntity>(request);
            await AdGroupStatsDbRepo.InsertAsync(entity);
            await AdGroupStatsDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
