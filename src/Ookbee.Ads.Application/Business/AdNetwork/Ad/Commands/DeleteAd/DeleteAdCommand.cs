using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.DeleteAd
{
    public class DeleteAdCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteAdCommand(long id)
        {
            Id = id;
        }
    }
}
