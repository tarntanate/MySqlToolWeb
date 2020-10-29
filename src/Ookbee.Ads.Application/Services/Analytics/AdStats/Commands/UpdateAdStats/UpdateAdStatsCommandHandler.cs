using AutoMapper;
using MediatR;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.UpdateAdStats
{
    public class UpdateAdStatsCommandHandler : IRequestHandler<UpdateAdStatsCommand>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public UpdateAdStatsCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdStatsEntity> adStatsDbRepo)
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
