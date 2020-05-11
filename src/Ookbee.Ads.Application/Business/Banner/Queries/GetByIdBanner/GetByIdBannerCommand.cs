using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetByIdBanner
{
    public class GetByIdBannerCommand : IRequest<HttpResult<BannerDto>>
    {
        public string Id { get; set; }

        public GetByIdBannerCommand(string id)
        {
            Id = id;
        }
    }
}
