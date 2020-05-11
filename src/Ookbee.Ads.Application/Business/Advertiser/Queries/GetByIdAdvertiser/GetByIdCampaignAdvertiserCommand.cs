using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetByIdAdvertiser
{
    public class GetByIdAdvertiserCommand : IRequest<HttpResult<AdvertiserDto>>
    {
        public string Id { get; set; }

        public GetByIdAdvertiserCommand(string id)
        {
            Id = id;
        }
    }
}
