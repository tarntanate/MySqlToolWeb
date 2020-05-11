using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Banner.Commands.DeleteBanner
{
    public class DeleteBannerCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteBannerCommand(string id)
        {
            Id = id;
        }
    }
}
