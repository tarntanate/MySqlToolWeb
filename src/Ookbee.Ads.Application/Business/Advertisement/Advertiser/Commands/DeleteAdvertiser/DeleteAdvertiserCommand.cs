using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteAdvertiserCommand(long id)
        {
            Id = id;
        }
    }
}
