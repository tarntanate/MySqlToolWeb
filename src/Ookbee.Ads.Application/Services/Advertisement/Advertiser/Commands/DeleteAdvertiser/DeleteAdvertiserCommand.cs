using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteAdvertiserCommand(long id)
        {
            Id = id;
        }
    }
}
