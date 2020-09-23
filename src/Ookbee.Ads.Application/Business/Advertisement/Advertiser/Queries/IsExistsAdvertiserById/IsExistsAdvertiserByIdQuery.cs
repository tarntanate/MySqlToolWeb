using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdvertiserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
