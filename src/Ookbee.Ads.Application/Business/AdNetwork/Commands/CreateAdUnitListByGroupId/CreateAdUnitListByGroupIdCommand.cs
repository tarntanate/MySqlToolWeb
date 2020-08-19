using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdUnitListByGroupId
{
    public class CreateAdUnitListByGroupIdCommand : IRequest<HttpResult<bool>>
    {
        public long AdGroupId { get; set; }

        public CreateAdUnitListByGroupIdCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
