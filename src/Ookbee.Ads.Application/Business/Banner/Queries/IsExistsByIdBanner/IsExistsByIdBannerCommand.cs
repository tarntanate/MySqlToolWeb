using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Banner.Queries.IsExistsByIdBanner
{
    public class IsExistsByIdBannerCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdBannerCommand(string id)
        {
            Id = id;
        }
    }
}
