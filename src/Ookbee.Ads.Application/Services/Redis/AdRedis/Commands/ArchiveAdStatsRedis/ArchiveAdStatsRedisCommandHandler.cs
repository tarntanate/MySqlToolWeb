using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdStat.Queries.GetAdStatsList;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsByIdRedis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsRedis
{
    public class ArchiveAdStatsRedisCommandHandler : IRequestHandler<ArchiveAdStatsRedisCommand>
    {
        private readonly IMediator Mediator;

        public ArchiveAdStatsRedisCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(ArchiveAdStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                var getAdStatsList = await Mediator.Send(new GetAdStatsListQuery(start, length, request.CaculatedAt, null, null), cancellationToken);
                if (getAdStatsList.IsSuccess)
                {
                    var adStatsList = getAdStatsList.Data;
                    foreach (var adStats in adStatsList)
                    {
                        await Mediator.Send(new ArchiveAdStatsByIdRedisCommand(request.CaculatedAt, adStats.AdId), cancellationToken);
                    }
                    next = adStatsList.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
