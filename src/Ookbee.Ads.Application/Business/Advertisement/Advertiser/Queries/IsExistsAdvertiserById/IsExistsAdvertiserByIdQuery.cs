using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdvertiserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
