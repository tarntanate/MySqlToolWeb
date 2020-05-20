using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetSignedUrl
{
    public class GetSignedUrlQuery : IRequest<HttpResult<string>>
    {
        public string MapperId { get; set; }
        
        public string FileExtension { get; set; }

        public GetSignedUrlQuery(string mapperId, GetSignedUrlQuery request)
        {
            MapperId = mapperId;
            FileExtension = request.FileExtension;
        }
    }
}
