using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Commands.DeleteAd
{
    public class DeleteAdCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteAdCommand(long id)
        {
            Id = id;
        }
    }
}
