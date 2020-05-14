using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommand : IRequest<HttpResult<string>>
    {
        public string Bucket { get; set; }

        public string Key { get; set; }
    }
}
