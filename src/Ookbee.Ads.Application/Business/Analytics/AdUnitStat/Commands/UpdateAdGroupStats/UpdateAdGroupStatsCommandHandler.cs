using AutoMapper;
using MediatR;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdUnitStatsCommandHandler : IRequestHandler<UpdateAdUnitStatsCommand, Unit>
    {
        private IMapper Mapper { get; }
        private AnalyticsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo { get; }

        public UpdateAdUnitStatsCommandHandler(
            IMapper mapper,
            AnalyticsDbRepository<AdUnitStatsEntity> adUnitStatsDbRepo)
        {
            Mapper = mapper;
            AdUnitStatsDbRepo = adUnitStatsDbRepo;
        }

        public async Task<Unit> Handle(UpdateAdUnitStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdUnitStatsEntity>(request);
            await AdUnitStatsDbRepo.UpdateAsync(entity.Id, entity);
            await AdUnitStatsDbRepo.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
