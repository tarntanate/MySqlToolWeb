using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAdUploadUrl
{
    public class CreateAdUploadUrlCommand : IRequest<HttpResult<AdUploadUrlDto>>
    {
        public string Id { get; set; }

        public string FileExtension { get; set; }

        public CreateAdUploadUrlCommand()
        {

        }

        public CreateAdUploadUrlCommand(string id, CreateAdUploadUrlCommand request)
        {
            Id = id;
            FileExtension = request.FileExtension;
        }
    }
}
