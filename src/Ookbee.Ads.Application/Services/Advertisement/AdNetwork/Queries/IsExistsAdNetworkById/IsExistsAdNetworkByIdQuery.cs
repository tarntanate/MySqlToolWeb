using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.IsExistsAdNetworkById
{
    public class IsExistsAdNetworkByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public IsExistsAdNetworkByIdQuery(long id)
        {
            Id = id;
        }
    }
}
