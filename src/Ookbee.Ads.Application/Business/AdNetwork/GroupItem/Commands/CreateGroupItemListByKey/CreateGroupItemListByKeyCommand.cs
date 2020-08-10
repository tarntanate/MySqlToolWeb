using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Commands.CreateGroupItemListByKey
{
    public class CreateGroupItemListByKeyCommand : IRequest<HttpResult<bool>>
    {
        public long AdGroupId { get; set; }

        public CreateGroupItemListByKeyCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
