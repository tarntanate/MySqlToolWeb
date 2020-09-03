using AutoMapper;
using MediatR;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.UpdateAdStats
{
    public class UpdateAdStatsCommandHandler : IRequestHandler<UpdateAdStatsCommand>
    {
        private IMapper Mapper { get; }
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public UpdateAdStatsCommandHandler(
            IMapper mapper,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mapper = mapper;
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Unit> Handle(UpdateAdStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdStatsEntity>(request);
            await AdStatsDbRepo.UpdateAsync(entity.Id, entity);
            await AdStatsDbRepo.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
