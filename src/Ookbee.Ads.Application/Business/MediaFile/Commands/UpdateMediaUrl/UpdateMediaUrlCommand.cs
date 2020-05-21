using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaUrl
{
    public class UpdateMediaUrlCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string MediaUrl { get; set; }

        public UpdateMediaUrlCommand()
        {

        }

        public UpdateMediaUrlCommand(string id, string mediaUrl)
        {
            Id = id;
            MediaUrl = mediaUrl;
        }
    }
}
