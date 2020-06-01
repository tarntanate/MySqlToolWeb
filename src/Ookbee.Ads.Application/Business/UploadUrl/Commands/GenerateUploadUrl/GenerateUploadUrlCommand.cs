using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommand : IRequest<HttpResult<UploadUrlDto>>
    {
        public string MapperId { get; set; }

        public string MapperType { get; set; }

        public string MimeType { get; set; }

        public string FileExtension { get; set; }

        public string Bucket { get; set; }

        public string Key { get; set; }

        public GenerateUploadUrlCommand(string mapperId, string mapperType, string fileExtension, string mimeType, string bucket, string key)
        {
            MapperId = mapperId;
            MapperType = mapperType;
            MimeType = mimeType;
            FileExtension = fileExtension;
            Bucket = bucket;
            Key = key;
        }
    }
}
