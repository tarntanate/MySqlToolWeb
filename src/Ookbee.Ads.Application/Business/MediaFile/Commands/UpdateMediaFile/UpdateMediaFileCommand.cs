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

        public string Position { get; set; }

        public UpdateMediaFileCommand()
        {

        }

        public UpdateMediaFileCommand(string id, UpdateMediaFileCommand request)
        {
            Id = id;
            AdId = request.AdId;
            Name = request.Name;
            Description = request.Description;
            Position = request.Position;
        }
    }
}
