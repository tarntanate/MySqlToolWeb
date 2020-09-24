using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsCommandHandler : IRequestHandler<CreateAdGroupStatsCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public CreateAdGroupStatsCommandHandler(
            IMapper mapper,
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            Mapper = mapper;
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupStatsEntity>(request);
            await AdGroupStatsDbRepo.InsertAsync(entity);
            await AdGroupStatsDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.OK(entity.Id);
        }
    }
}
