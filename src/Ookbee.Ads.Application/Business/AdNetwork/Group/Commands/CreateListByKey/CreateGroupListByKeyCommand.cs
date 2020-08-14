using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Group.Commands.CreateGroupListByKey
{
    public class CreateGroupListByKeyCommand : IRequest<HttpResult<bool>>
    {
        public long AdGroupId { get; set; }

        public CreateGroupListByKeyCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
