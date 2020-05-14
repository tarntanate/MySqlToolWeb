using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.CreateMediaFile
{
    public class CreateMediaFileCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();

        public string BannerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MediaType { get; set; }

        public string MediaUrl { get; set; }

        public string LinkUrl { get; set; }

        public string Position { get; set; }

        public CreateMediaFileCommand()
        {
            
        }

        public CreateMediaFileCommand(CreateMediaFileCommand request)
        {
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
