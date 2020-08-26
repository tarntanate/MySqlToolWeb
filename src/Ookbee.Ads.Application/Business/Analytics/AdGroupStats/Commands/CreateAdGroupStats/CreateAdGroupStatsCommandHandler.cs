using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStats.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsCommandHandler : IRequestHandler<CreateAdGroupStatsCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private AnalyticsDbRepository<AdGroupStatsEntity> RequestLogDbRepo { get; }

        public CreateAdGroupStatsCommandHandler(
            IMapper mapper,
            AnalyticsDbRepository<AdGroupStatsEntity> requestLogDbRepo)
        {
            Mapper = mapper;
            RequestLogDbRepo = requestLogDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdGroupStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupStatsEntity>(request);
            await RequestLogDbRepo.InsertAsync(entity);
            await RequestLogDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<long>();
            return result.Success(entity.Id);
        }
    }
}
