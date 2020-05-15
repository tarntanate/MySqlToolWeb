using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Banner.Queries.IsExistsBannerById
{
    public class IsExistsBannerByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsBannerByIdQuery(string id)
        {
            Id = id;
        }
    }
}
