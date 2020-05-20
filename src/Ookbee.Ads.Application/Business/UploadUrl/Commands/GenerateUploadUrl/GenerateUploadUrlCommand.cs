using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommand : IRequest<HttpResult<string>>
    {
        public ObjectId MapperId { get; set; }

        public string Bucket { get; set; }

        public string Key { get; set; }

        public GenerateUploadUrlCommand(ObjectId mapperId, string bucket, string key)
        {
            MapperId = mapperId;
            Bucket = bucket;
            Key = key;
        }
    }
}
