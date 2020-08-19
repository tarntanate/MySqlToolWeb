using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdUnitById
{
    public class CreateAdUnitByIdCommand : IRequest<HttpResult<bool>>
    {
        public long AdId { get; set; }

        public CreateAdUnitByIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
