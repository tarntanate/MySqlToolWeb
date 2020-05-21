using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string AdId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MimeType { get; set; }

        public string MediaUrl { get; set; }

        public string Position { get; set; }

        public string AppLink { get; set; }

        public string WebLink { get; set; }

        public bool EnabledFlag => true;

        public UpdateMediaFileCommand()
        {

        }

        public UpdateMediaFileCommand(string id, UpdateMediaFileCommand request)
        {
            Id = id;
            AdId = request.AdId;
            Name = request.Name;
            Description = request.Description;
            MimeType = request.MimeType;
            MediaUrl = request.MediaUrl;
            Position = request.Position;
            AppLink = request.AppLink;
            WebLink = request.WebLink;
        }
    }
}
