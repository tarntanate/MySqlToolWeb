using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public IsExistsAdvertiserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
