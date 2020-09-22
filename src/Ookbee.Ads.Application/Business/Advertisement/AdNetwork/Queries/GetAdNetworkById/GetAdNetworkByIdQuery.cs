using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.GetAdNetworkById
{
    public class GetAdNetworkByIdQuery : IRequest<Response<AdNetworkDto>>
    {
        public long Id { get; set; }

        public GetAdNetworkByIdQuery(long id)
        {
            Id = id;
        }
    }
}
