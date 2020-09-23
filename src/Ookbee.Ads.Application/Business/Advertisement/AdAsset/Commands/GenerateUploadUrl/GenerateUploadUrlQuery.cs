using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommand : IRequest<Response<string>>
    {
        public long Id { get; set; }
        public string Extension { get; set; }

        public GenerateUploadUrlCommand(long id, string extension)
        {
            Id = id;
            Extension = extension;
        }
    }
}
