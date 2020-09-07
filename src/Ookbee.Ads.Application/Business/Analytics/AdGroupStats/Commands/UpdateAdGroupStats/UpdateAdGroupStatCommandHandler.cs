using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.UpdateAdGroupStats
{
    public class UpdateAdGroupStatsCommandHandler : IRequestHandler<UpdateAdGroupStatsCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public UpdateAdGroupStatsCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupStatsEntity>(request);
            await AdGroupStatsDbRepo.InsertAsync(entity);
            await AdGroupStatsDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
