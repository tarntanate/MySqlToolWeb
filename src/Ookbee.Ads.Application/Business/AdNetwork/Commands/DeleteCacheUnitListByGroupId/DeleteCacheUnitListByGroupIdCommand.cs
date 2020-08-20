using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheUnitListByGroupId
{
    public class DeleteCacheUnitListByGroupIdCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public DeleteCacheUnitListByGroupIdCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
