using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFileUrl
{
    public class UpdateFileUrlCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }
        public string FileUrl { get; set; }

        public UpdateFileUrlCommand(string id, string fileUrl)
        {
            Id = id;
        }
    }
}
