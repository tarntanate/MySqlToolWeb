using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Queries.GetAdvertiserById
{
    public class GetAdvertiserByIdQuery : IRequest<HttpResult<AdvertiserDto>>
    {
        public long Id { get; set; }

        public GetAdvertiserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
