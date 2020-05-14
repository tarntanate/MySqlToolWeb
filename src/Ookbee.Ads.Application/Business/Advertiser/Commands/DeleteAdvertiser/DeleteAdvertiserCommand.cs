using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteAdvertiserCommand()
        {
            
        }

        public DeleteAdvertiserCommand(string id)
        {
            Id = id;
        }
    }
}
