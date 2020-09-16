using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.DeleteAdStats
{
    public class DeleteAdStatsCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public DeleteAdStatsCommand(long adId)
        {
            AdId = adId;
        }
    }
}
