using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.DeleteAdNetwork
{
    public class DeleteAdNetworkCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteAdNetworkCommand(long id)
        {
            Id = id;
        }
    }
}
