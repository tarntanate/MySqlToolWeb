using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommand : IRequest<HttpResult<string>>
    {
        public string MapperId { get; set; }

        public string Bucket { get; set; }

        public string Key { get; set; }

        public GenerateUploadUrlCommand(string mapperId, string bucket, string key)
        {
            MapperId = mapperId;
            Bucket = bucket;
            Key = key;
        }
    }
}
