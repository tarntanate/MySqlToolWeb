using AutoMapper;
using MediatR;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdUnitStatsCommandHandler : IRequestHandler<UpdateAdUnitStatsCommand>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo;

        public UpdateAdUnitStatsCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdUnitStatsEntity> adUnitStatsDbRepo)
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
