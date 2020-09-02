using AutoMapper;
using MediatR;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Commands.UpdateAdGroupStats
{
    public class UpdateAdGroupStatsCommandHandler : IRequestHandler<UpdateAdGroupStatsCommand, Unit>
    {
        private IMapper Mapper { get; }
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public UpdateAdGroupStatsCommandHandler(
            IMapper mapper,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mapper = mapper;
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Unit> Handle(UpdateAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupStatsEntity>(request);
            await AdGroupStatsDbRepo.UpdateAsync(entity.Id, entity);
            await AdGroupStatsDbRepo.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
