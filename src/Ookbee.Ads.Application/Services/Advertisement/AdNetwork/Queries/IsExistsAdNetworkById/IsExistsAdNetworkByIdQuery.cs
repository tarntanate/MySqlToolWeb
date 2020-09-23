using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.IsExistsAdNetworkById
{
    public class IsExistsAdNetworkByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdNetworkByIdQuery(long id)
        {
            Id = id;
        }
    }
}
