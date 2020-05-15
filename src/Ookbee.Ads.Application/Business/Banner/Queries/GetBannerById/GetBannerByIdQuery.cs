using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerById
{
    public class GetBannerByIdQuery : IRequest<HttpResult<BannerDto>>
    {
        public string Id { get; set; }

        public GetBannerByIdQuery(string id)
        {
            Id = id;
        }
    }
}
