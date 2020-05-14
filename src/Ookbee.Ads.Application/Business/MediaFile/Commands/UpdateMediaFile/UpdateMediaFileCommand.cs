using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string BannerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MediaType { get; set; }

        public string MediaUrl { get; set; }

        public string LinkUrl { get; set; }

        public string Position { get; set; }

        public UpdateMediaFileCommand(string id)
        {
            Id = id;
        }

        public UpdateMediaFileCommand(string id, UpdateMediaFileCommand request)
        {
            Id = id;
            BannerId = request.BannerId;
            Name = request.Name;
            Description = request.Description;
            MediaType = request.MediaType;
            MediaUrl = request.MediaUrl;
            LinkUrl = request.LinkUrl;
            Position = request.Position;
        }
    }
}
