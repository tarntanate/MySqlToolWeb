using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.CreateUploadUrl
{
    public class CreateUploadUrlCommand : IRequest<HttpResult<SignedUrlDto>>
    {
        public string Id { get; set; }

        public string FileExtension { get; set; }

        public CreateUploadUrlCommand()
        {

        }

        public CreateUploadUrlCommand(string id, CreateUploadUrlCommand request)
        {
            Id = id;
            FileExtension = request.FileExtension;
        }
    }
}
