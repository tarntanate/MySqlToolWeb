using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.GetAdNetworkById
{
    public class GetAdNetworkByIdQuery : IRequest<HttpResult<AdNetworkDto>>
    {
        public long Id { get; set; }

        public GetAdNetworkByIdQuery(long id)
        {
            Id = id;
        }
    }
}
