using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.GetAdById
{
    public class GetAdByIdQuery : IRequest<HttpResult<AdDto>>
    {
        public long Id { get; set; }

        public GetAdByIdQuery(long id)
        {
            Id = id;
        }
    }
}
