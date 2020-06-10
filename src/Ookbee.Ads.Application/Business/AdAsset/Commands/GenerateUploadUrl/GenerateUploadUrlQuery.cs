using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommand : IRequest<HttpResult<string>>
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
