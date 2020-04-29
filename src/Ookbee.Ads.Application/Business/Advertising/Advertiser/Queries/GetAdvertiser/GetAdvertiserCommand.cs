using MediatR;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.ViewModels;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.GetAdvertiser
{
    public class GetAdvertiserCommand : IRequest<HttpResult<AdvertiserViewModel>>
    {
        public string Id { get; set; }
        
        public GetAdvertiserCommand(string id)
        {
            Id = id;
        }
    }
}
