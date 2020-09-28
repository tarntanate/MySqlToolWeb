using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.CreateAdStats
{
    public class CreateAdStatsCommandHandler : IRequestHandler<CreateAdStatsCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public CreateAdStatsCommandHandler(
            IMapper mapper,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mapper = mapper;
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdStatsEntity>(request);
            await AdStatsDbRepo.InsertAsync(entity);
            await AdStatsDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.OK(entity.Id);
        }
    }
}
