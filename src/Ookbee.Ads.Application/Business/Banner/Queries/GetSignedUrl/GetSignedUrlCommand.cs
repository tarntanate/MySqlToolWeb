using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetSignedUrl
{
    public class GetSignedUrlCommand : IRequest<HttpResult<string>>
    {
        public string FileName => ObjectId.GenerateNewId().ToString();
        
        public string FileExtension { get; set; }
    }
}
