using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.DeleteAdStats
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
