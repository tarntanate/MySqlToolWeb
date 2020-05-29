using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.CreateMediaFile
{
    public class CreateMediaFileCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();

        public string AdId { get; set; }

        public string Position { get; set; }

        public bool EnabledFlag => true;

        public CreateMediaFileCommand()
        {

        }

        public CreateMediaFileCommand(CreateMediaFileCommand request)
        {
            AdId = request.AdId;
            Position = request.Position;
        }
    }
}
