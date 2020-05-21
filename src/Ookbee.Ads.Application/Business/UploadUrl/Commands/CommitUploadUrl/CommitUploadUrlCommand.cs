using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; }

        public string Bucket { get; set; }

        public string Key { get; set; }

        public CommitUploadUrlCommand(string id, string bucket, string key)
        {
            Id = id;
            Bucket = bucket;
            Key = key;
        }
    }
}
