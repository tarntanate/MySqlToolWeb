using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkById
{
    public class GetAdNetworkByIdQuery : IRequest<Response<AdNetworkDto>>
    {
        public long Id { get; private set; }

        public GetAdNetworkByIdQuery(long id)
        {
            Id = id;
        }
    }
}
