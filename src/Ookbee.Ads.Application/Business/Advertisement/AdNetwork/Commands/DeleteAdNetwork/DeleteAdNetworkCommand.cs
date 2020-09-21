using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.DeleteAdNetwork
{
    public class DeleteAdNetworkCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteAdNetworkCommand(long id)
        {
            Id = id;
        }
    }
}
